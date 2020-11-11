using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Resource;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.Steps
{
    [Binding]
    public class LookForARoommateSteps: BaseTest
    {
        private string StudentEndPoint { get; set; }
        private StudentResource Roommate { get; set; }


        public LookForARoommateSteps()
        {
            StudentEndPoint = $"{ApiUri}api";
        }

        [Given(@"the student log in the platform")]
        public void GivenTheStudentLogInThePlatform(Table table)
        {
            try
            {
                var student = table.CreateInstance<SaveStudentResource>();
                var data = JsonData(student);
                var result = Task.Run(async () => await Client.PostAsync($"{StudentEndPoint}/students", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add Student Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }


        [Given(@"the student enter to the Roommate section")]
        public void WhenTheStudentEnterToTheRoommateSection()
        {
            StudentEndPoint = $"{StudentEndPoint}/students"; 
        }
        
        [Then(@"the student will see the list of roommates")]
        public void ThenTheStudentWillSeeTheListOfRoommates()
        {
            var result = Task.Run(async () => await Client.GetAsync(StudentEndPoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Students Integration Test Completed");
        }

        [When(@"the student click on Ver más option of the roommate with id (.*)")]
        public void WhenTheStudentClickOnVerMasOptionOfTheRoommateWithId(int p0)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{StudentEndPoint}/1")).Result;
            Roommate = ObjectData<StudentResource>(result.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Student Integration Test Completed");
        }

        [Then(@"will appears the roommate's information in the Perfil section")]
        public void ThenWillAppearsTheRoommateSInformationInThePerfilSection(Table table)
        {
            var expectedRoommate = table.CreateInstance<StudentResource>();
            Assert.AreEqual(expectedRoommate.Dni, Roommate.Dni);
        }

        [When(@"the student click on Send Invitation button")]
        public void WhenTheStudentClickOnSendInvitationButton()
        {
            StudentEndPoint = $"{StudentEndPoint}/3/friendshiprequest";
        }

        [Then(@"the student see a message ""(.*)""")]
        public void ThenTheStudentSeeAMessage(string p0)
        {
            string message;
            try
            {
                var friendrequest = new RequestAnswerResource
                {
                    Answer = 1
                };
                var data = JsonData(friendrequest);

                var result = Task.Run(async () => await Client.PostAsync($"{StudentEndPoint}/{Roommate.Id}",data)).Result;

                if (result != null && result.StatusCode == HttpStatusCode.OK)
                    message = "Wait for your possible roommate answer";
                else
                    message = "Error";

                Assert.AreEqual(p0, message);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
            
        }


    }
}

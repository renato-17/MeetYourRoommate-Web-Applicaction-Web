using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Resource;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowTest.Steps
{
    [Binding]
    public class ResponseFriendshipRequestSteps: BaseTest
    {
        
        private string RequestEndPoint { get; set; }
        private StudentResource Student { get; set; }
        private List<RequestResource> Requests { get; set; }
        private RequestResource Request { get; set; }

        public ResponseFriendshipRequestSteps()
        {
            RequestEndPoint = $"{ApiUri}api/students";
        }

        [Given(@"the student log in to the platform")]
        public void GivenTheStudentLogInToThePlatform()
        {
            var result = Task.Run(async () => await Client.GetAsync($"{RequestEndPoint}/1")).Result;
            Student = ObjectData<StudentResource>(result.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Student Integration Test Completed");
        }
        
        [Given(@"the student select the Friendship Request section")]
        public void GivenTheStudentSelectTheFriendshipRequestSection()
        {
            RequestEndPoint = $"{RequestEndPoint}/{Student.Id}/friendshiprequest";
        }
        
        [When(@"the student accept the first friendship request")]
        public void WhenTheStudentAcceptTheFriendshipRequestWithId()
        {
            var result = Task.Run(async () => await Client.GetAsync($"{RequestEndPoint}/received")).Result;
            Requests = ObjectData<List<RequestResource>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Students Integration Test Completed");

            Request = Requests[0];
            Assert.IsNotNull(Request);
        }
        
        [Then(@"the student will be her/his request accepted")]
        public void ThenTheStudentWillBeHerHisRequestAccepted()
        {
            try
            {
                var friendrequest = new RequestAnswerResource
                {
                    Answer = 1
                };
                var data = JsonData(friendrequest);

                var result = Task.Run(async () => await Client.PutAsync($"{RequestEndPoint}/{Request.PersonOneId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Answer request Integration Test Completed");

                var result1 = Task.Run(async () => await Client.DeleteAsync($"{ApiUri}api/students/{Request.PersonOneId}/friendshiprequest/{Request.PersonTwoId}")).Result;
                Assert.IsTrue(result1 != null && result1.StatusCode == HttpStatusCode.OK, "Answer request Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }
    }
}

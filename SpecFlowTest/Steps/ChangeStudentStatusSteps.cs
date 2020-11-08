using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Domain.Models;
using Roommates.API.Resource;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowTest.Steps
{
    [Binding]

    public class ChangeStudentStatusSteps:BaseTest
    {

        private string StudentEndPoint { get; set; }
        private Student Student { get; set; }
        public ChangeStudentStatusSteps()
        {
            StudentEndPoint = $"{ApiUri}api";
        }

        [Given(@"the student login to the platform with id (.*)")]
        public void GivenTheStudentLoginToThePlatformWithId(int p0)
        {
            StudentEndPoint = $"{StudentEndPoint}/students/{p0}";
        }


        [Given(@"the student enter to Profile section")]
        public void GivenTheStudentEnterToProfileSection()
        {
            var result = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync(StudentEndPoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
            
            Student = ObjectData<Student>(result.Content.ReadAsStringAsync().Result);
        }
        
        [When(@"the student select the Finish Searching option")]
        public void WhenTheStudentSelectTheFinishSearchingOption()
        {
            Student.Available = false;
        }
        
        [Then(@"the student will se her/his status changed")]
        public void ThenTheStudentWillSeHerHisStatusChanged()
        {
            Assert.IsFalse(Student.Available);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.Steps
{
    [Binding]
    public class LessorSteps : BaseTest
    {
        private string LessorEndPoint { get; set; }
        HttpResponseMessage message;
        public LessorSteps()
        {
            LessorEndPoint = $"{ApiUri}api/lessors";

        }

        [Given(@"I know the lessor's id")]
        public void GivenIKnowTheLessorSDni()
        {
        }
        [When(@"I supplied (.*) as lessor id")]
        public void WhenISuppliedAsLessorDni(int p0)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{LessorEndPoint}/{p0}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Lessor Integration Test Completed");
        }

        [Then(@"lessor details should be")]
        public void ThenLessorDetailsShouldBe(Table dto)
        {
            var lessor = dto.CreateInstance<Roommates.API.Domain.Models.Lessor>();
            var result = Task.Run(async () => await Client.GetAsync($"{LessorEndPoint}/{lessor.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Lessor Details Integration Test Completed");
            var lessorToComparte = ObjectData<Roommates.API.Domain.Models.Lessor>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(lessorToComparte));
        }

        [Given(@"I want to add a new lessor")]
        public void GivenIWantToAddANewLessor()
        {
            
        }

        [When(@"lessor attributes provided")]
        public void WhenLessorAttributesProvided(Table dto)
        {
            var lessor = dto.CreateInstance<Roommates.API.Domain.Models.Lessor>();
            var data = JsonData(lessor);
            message = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(LessorEndPoint, data)).Result;
        }

        [Then(@"lessor will be able to enter to the platform as lessor")]
        public void ThenLessorWillBeAbleToEnterToThePlatformAsLessor()
        {
            Assert.IsTrue(message != null && message.StatusCode == HttpStatusCode.OK, "Add Lessor Integration Test Completed");
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.Steps
{
    [Binding]
    public class PropertySteps : BaseTest
    {
        private string PropertyEndPoint { get; set; }
        HttpResponseMessage message;
        public PropertySteps()
        {
            PropertyEndPoint = $"{ApiUri}api/lessors/1/properties";
        }
        [Given(@"I want to see the description of a property")]
        public void GivenIWantToSeeTheDescriptionOfAProperty()
        {
            
        }
        [When(@"I supplied (.*) as property id")]
        public void WhenISuppliedAsPropertyId(int p0)
        {
            var result = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{PropertyEndPoint}/{1}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Property Integration Test Completed");
        }

        [Then(@"property details should be")]
        public void ThenPropertyDetailsShouldBe(Table dto)
        {
            var property = dto.CreateInstance<Property>();
            var result = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{PropertyEndPoint}/{property.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Property Details Integration Test Completed");
            var propertyToComparte = ObjectData<Property>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(propertyToComparte));
        }



        [Given(@"I want to add a new property")]
        public void GivenIWantToAddANewProperty()
        {
        }
        [When(@"property attributes provided")]
        public void WhenPropertyAttributesProvided(Table dto)
        {
            var property = dto.CreateInstance<Property>();
            var data = JsonData(property);
            message = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync($"{ PropertyEndPoint}/", data)).Result;
        }

        [Then(@"property was created")]
        public void ThenPropertyWasCreated()
        {
            Assert.IsTrue(message != null && message.StatusCode == HttpStatusCode.OK, "Add Property Integration Test Completed");
        }
    }
}

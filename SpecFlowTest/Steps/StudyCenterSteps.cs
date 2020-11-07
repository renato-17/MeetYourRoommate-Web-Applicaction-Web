using System;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Roommates.API.Domain.Models;

namespace SpecFlowTest.Steps
{
    [Binding]
    class StudyCenterSteps : BaseTest
    {
        private string StudyCenterEndPoint { get; set; }
        public StudyCenterSteps()
        {
            StudyCenterEndPoint = $"{ApiUri}api/studycenters";
        }
        [When(@"studyCenter required attributes provided")]
        public void WhenStudyCenterRequiredAttributesProvided(Table dto)
        {
            try
            {
                var studyCenter = dto.CreateInstance<StudyCenter>();
                var data = JsonData(studyCenter);
                var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(StudyCenterEndPoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

    }
}

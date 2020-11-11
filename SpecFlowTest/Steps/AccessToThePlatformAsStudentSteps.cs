using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Resource;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.Steps
{
    [Binding]
    class AccessToThePlatformAsStudentSteps: BaseTest
    {
        private string StudentEndPoint { get; set; }
        private SaveStudentResource ActualStudent { get; set; }
        private CampusResource ActualCampus { get; set; }
        private StudyCenterResource ActualStudyCenter { get; set; }

        private int StudentId { get; set; }

        public AccessToThePlatformAsStudentSteps()
        {
            StudentEndPoint = $"{ApiUri}api/students";
        }

        [Given(@"the person enter to the platform")]
        public void GivenThePersonEnterToThePlatform()
        {
            Assert.IsTrue(true);
        }

        [Given(@"enter her/his information")]
        public void GivenEnterHerHisInformation(Table table)
        {
            ActualStudent = table.CreateInstance<SaveStudentResource>();
            Assert.IsNotNull(ActualStudent);

        }

        [Given(@"specify her/his study center")]
        public void GivenSpecifyHerHisStudyCenter(Table table)
        {

            var studyCenterEndPoint = $"{ApiUri}api/studycenters";
            try
            {
                var StudyCenterResource = table.CreateInstance<SaveStudyCenterResource>();
                var data = JsonData(StudyCenterResource);
                var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(studyCenterEndPoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");


                var resource = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{studyCenterEndPoint}/1")).Result;
                ActualStudyCenter = ObjectData<StudyCenterResource>(resource.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(1, ActualStudyCenter.Id);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }

        }

        [Given(@"specify her/his campus")]
        public void GivenSpecifyHerHisCampus(Table table)
        {

            try
            {
                var campusEndPoint = $"{ApiUri}api/studycenters/{ActualStudyCenter.Id}/campuses";
                var campus = table.CreateInstance<SaveCampusResource>();
                var data = JsonData(campus);
                var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(campusEndPoint, data)).Result;


                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add Campus Integration Test Completed");

                var resource = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{campusEndPoint}/1")).Result;
                ActualCampus = ObjectData<CampusResource>(resource.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(1, ActualCampus.Id);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [When(@"he/she click on register")]
        public void WhenHeSheClickOnRegister()
        {
            ActualStudent.StudyCenterId = ActualStudyCenter.Id;
            ActualStudent.CampusId = ActualCampus.Id;

            try
            {
                var data = JsonData(ActualStudent);
                var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(StudentEndPoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");


            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [Then(@"he/she will be able to enter to the platform as student")]
        public void ThenHeSheWillBeAbleToEnterToThePlatformAsStudent()
        {

            try
            {
                var result = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{StudentEndPoint}/1")).Result;
                var student = ObjectData<StudentResource>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");

                Assert.AreEqual("Juan Perez", $"{student.FirstName} {student.LastName}");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }
    }
}

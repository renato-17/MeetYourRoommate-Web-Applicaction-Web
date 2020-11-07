using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roommates.API.Domain.Models;
using Roommates.API.Resource;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.Steps
{
    [Binding]
    public class EnterToThePlatformAsStudentSteps: BaseTest
    {
        private string StudentEndPoint { get; set; }
        private Student ActualStudent { get; set; }
        private Campus ActualCampus { get; set; }
        private StudyCenterResource ActualStudyCenter { get; set; }

        public EnterToThePlatformAsStudentSteps()
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
            ActualStudent = table.CreateInstance<Student>();
            Assert.IsNotNull(ActualStudent);
           
        }
        
        [Given(@"specify her/his study center")]
        public void GivenSpecifyHerHisStudyCenter(Table table)
        {
            
            var studyCenterEndPoint = $"{ApiUri}api/studycenters";
            try
            {
                var StudyCenterResource = table.CreateInstance<SaveStudyCenterResource>();
                var data = JsonData(ActualStudyCenter);
                var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(studyCenterEndPoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");


                
                var resource = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync($"{studyCenterEndPoint}/1")).Result;
                ActualStudyCenter = ObjectData<StudyCenterResource>(resource.Content.ReadAsStringAsync().Result);

                Assert.IsTrue(ActualStudyCenter.Id == 1);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }

        }

        //[Given(@"specify her/his campus")]
        //public void GivenSpecifyHerHisCampus(Table table)
        //{

        //    var campusEndPoint = $"{ApiUri}api/studyCenters/1/campuses";
        //    try
        //    {
        //        ActualCampus = table.CreateInstance<SaveCampusResource>();

        //        var data = JsonData(ActualCampus);
        //        var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(campusEndPoint, data)).Result;
        //        Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsTrue(false, ex.Message);
        //    }
        //}

        //[When(@"he/she click on register")]
        //public void WhenHeSheClickOnRegister()
        //{
        //    ActualStudent.CampusId = ActualCampus.Id;
        //    ActualStudent.Campus = ActualCampus;
        //    try
        //    {
        //        var student = ActualStudent;
        //        var data = JsonData(student);
        //        var result = System.Threading.Tasks.Task.Run(async () => await Client.PostAsync(StudentEndPoint, data)).Result;
        //        Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsTrue(false, ex.Message);
        //    }
        //}

        //[Then(@"he/she will be able to enter to the platform as student")]
        //public void ThenHeSheWillBeAbleToEnterToThePlatformAsStudent()
        //{
        //    StudentEndPoint += $"/{ActualStudent.Id}";
        //    try
        //    {
        //        var result = System.Threading.Tasks.Task.Run(async () => await Client.GetAsync(StudentEndPoint)).Result;
        //        Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Add StudyCenter Integration Test Completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsTrue(false, ex.Message);
        //    }
        //}
    }
}

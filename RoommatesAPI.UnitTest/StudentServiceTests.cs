using FluentAssertions;
using Moq;
using NUnit.Framework;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Test
{
    public class StudentServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task GetByIdAsynWhenIdNoExistsReturnsNotFoundException()
        {
            //Arrange
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            int studentId = 1;
            mockStudentRepository.Setup(s => s.FindById(studentId))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            var mockCampusRepository = GetDefaultICampusRepositoryInstance();

            var service = new StudentService(mockStudentRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object, mockCampusRepository.Object);

            //Act
            StudentResponse result = await service.GetByIdAsync(studentId);
            var message = result.Message;


            //Assert
            message.Should().Be("Student not found");
        }

        [Test]
        public async System.Threading.Tasks.Task GetStudentsByTeamIdWhenTeamExistsShouldReturnAllStudents()
        {

            //Arrange
            var team = new Team { Id = 1, Name = "UPC" };
            var student1 = new Student { Id = 1, Team = team, TeamId = team.Id };
            var student2 = new Student { Id = 2, Team = team, TeamId = team.Id };
            var student3 = new Student { Id = 3, Team = team, TeamId = team.Id };

            List<Student> list = new List<Student>();
            list.Add(student1);
            list.Add(student2);
            list.Add(student3);

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();

            mockStudentRepository.Setup(s => s.ListByTeamIdAsync(team.Id))
                .Returns(System.Threading.Tasks.Task.FromResult<IEnumerable<Student>>(list));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            var mockCampusRepository = GetDefaultICampusRepositoryInstance();

            var service = new StudentService(mockStudentRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object, mockCampusRepository.Object);


            //Act
            var result = await service.GetAllStudentsByTeamId(team.Id);
            var numStudents = result.ToList().Count;

            //Assert
            numStudents.Should().Be(3);
        }

        [Test]
        public async System.Threading.Tasks.Task JoinATeamWhenTeamNoExistReturnsJoinNewTeamMessage()
        {

            //Arrange
            var teamName = "UPC";
            var studentId = 1;

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();

            mockStudentRepository.Setup(s => s.FindById(studentId))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(new Student { Id = studentId }));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(s => s.FindByName(teamName))
               .Returns(System.Threading.Tasks.Task.FromResult<Team>(null));
            var mockCampusRepository = GetDefaultICampusRepositoryInstance();

            var service = new StudentService(mockStudentRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object, mockCampusRepository.Object);


            //Act
            StudentResponse result = await service.JoinTeam(new Team { Name = teamName }, studentId);
            var message = result.Message;

            //Assert
            message.Should().Be("Join to a new team succesfully");
        }

        [Test]
        public async System.Threading.Tasks.Task JoinATeamWhenTeamExistReturnsJoinTeamMessage()
        {

            //Arrange
            var teamName = "UPC";
            var studentId = 1;

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();

            mockStudentRepository.Setup(s => s.FindById(studentId))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(new Student { Id = studentId }));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(s => s.FindByName(teamName))
               .Returns(System.Threading.Tasks.Task.FromResult<Team>(new Team { Name = teamName }));

            var mockCampusRepository = GetDefaultICampusRepositoryInstance();

            var service = new StudentService(mockStudentRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object, mockCampusRepository.Object);


            //Act
            StudentResponse result = await service.JoinTeam(new Team { Name = teamName }, studentId);
            var message = result.Message;

            //Assert
            message.Should().Be("Join the team succesfully");
        }

        [Test]
        public async System.Threading.Tasks.Task LeaveATeamWhenHaveTeamReturnsAStudentWithoutTeam()
        {

            //Arrange
            var team = new Team { Id = 1, Name = "UPC" };
            var student = new Student { Id = 1, Team = team, TeamId = team.Id };


            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();

            mockStudentRepository.Setup(s => s.FindById(student.Id))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(student));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();

            var mockCampusRepository = GetDefaultICampusRepositoryInstance();

            var service = new StudentService(mockStudentRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object, mockCampusRepository.Object);


            //Act
            StudentResponse result = await service.LeaveTeam(student.Id);
            var resource = result.Resource.Team;

            //Assert
            resource.Should().Be(null);
        }

        private Mock<IStudentRepository> GetDefaultIStudentRepositoryInstance()
        {
            return new Mock<IStudentRepository>();
        }
        private Mock<ICampusRepository> GetDefaultICampusRepositoryInstance()
        {
            return new Mock<ICampusRepository>();
        }
        private Mock<ITeamRepository> GetDefaultITeamRepositoryInstance()
        {
            return new Mock<ITeamRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
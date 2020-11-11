using FluentAssertions;
using Moq;
using NUnit.Framework;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roommates.API.Test
{
    public class TeamServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task GetByIdAsynWhenIdNoExistsReturnsNotFoundException()
        {
            //Arrange
            var teamId = 1;

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(t => t.FindById(teamId))
                .Returns(System.Threading.Tasks.Task.FromResult<Team>(null));

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
     
            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object,mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByIdAsync(teamId);
            var message = result.Message;


            //Assert
            message.Should().Be("Team not found");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByStudentIdAsynWhenIdNoExistsReturnsNotFoundException()
        {
            //Arrange
            var studentId = 1;

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            mockStudentRepository.Setup(s => s.FindById(studentId))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object, mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByStudentId(studentId);
            var message = result.Message;


            //Assert
            message.Should().Be($"Student with id: {studentId} not found");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByStudentIdAsynWhenIdExistsButNotHaveTeamReturnsNotFoundException()
        {
            //Arrange
            var student = new Student { Id = 1 };

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            mockStudentRepository.Setup(s => s.FindById(student.Id))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(student));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object, mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByStudentId(student.Id);
            var message = result.Message;


            //Assert
            message.Should().Be("Student does not have team");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByStudentIdAsynWhenIdExistsButTeamIdExistsReturnsTeam() 
        {
            //Arrange
            Team team = new Team { Id = 1, Name = "UPC" };
            Student student = new Student { Id = 1, TeamId = 1 };

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(t => t.FindById(student.TeamId.Value))
                .Returns(System.Threading.Tasks.Task.FromResult<Team>(team));

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            mockStudentRepository.Setup(s => s.FindById(student.Id))
                .Returns(System.Threading.Tasks.Task.FromResult<Student>(student));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object, mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByStudentId(student.Id);
            var nameTeam = result.Resource.Name;


            //Assert
            nameTeam.Should().Be("UPC");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByNameAsyncWhenNameNoExistsReturnsNotFoundException()
        {
            //Arrange
            var team = new Team { Name = "Friends" };

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(t => t.FindByName(team.Name))
                .Returns(System.Threading.Tasks.Task.FromResult<Team>(null));

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object, mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByNameAsync(team);
            var message = result.Message;


            //Assert
            message.Should().Be("Team not found");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByNameAsyncWhenNameExistsReturnsTeam()
        {
            //Arrange
            var team = new Team { Name = "Friends" };

            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            mockTeamRepository.Setup(t => t.FindByName(team.Name))
                .Returns(System.Threading.Tasks.Task.FromResult<Team>(team));

            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TeamService(mockTeamRepository.Object, mockUnitOfWork.Object, mockStudentRepository.Object);

            //Act
            TeamResponse result = await service.GetByNameAsync(team);
            var nameTeam = result.Resource.Name;


            //Assert
            nameTeam.Should().Be("Friends");
        }

        private Mock<IStudentRepository> GetDefaultIStudentRepositoryInstance()
        {
            return new Mock<IStudentRepository>();
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

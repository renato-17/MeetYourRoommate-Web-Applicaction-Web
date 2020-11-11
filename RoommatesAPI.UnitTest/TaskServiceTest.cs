using Moq;
using NUnit.Framework;
using Roommates.API.Domain.Repositories;
using Roommates.API.Services;
using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Roommates.API.Test
{
    public class TaskServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task GetByIdAndTeamIdWhenAnyOfTheseNoExistsReturnsNotFoundException()
        {
            //Arrange
            var taskId = 1;
            var teamId = 1;

            var mockTaskRepository = GetDefaultITaskRepositoryInstance();
            mockTaskRepository.Setup(t => t.FindByIdAndTeamId(taskId, teamId))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Models.Task>(null));
            var mockTeamRepository = GetDefaultITeamRepositoryInstance();
            
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object);

            //Act
            var result = await service.GetByIdAndTeamIdAsync(taskId, teamId);
            var message = result.Message;

            //Assert
            message.Should().Be("Task or Team not found");
        }

        [Test]
        public async System.Threading.Tasks.Task GetByIdAndTeamIdWhenBothExistsReturnsTask()
        {
            //Arrange
            var taskId = 1;
            var teamId = 1;
            var description = "Clean the bathroom";

            var mockTaskRepository = GetDefaultITaskRepositoryInstance();
            mockTaskRepository.Setup(t => t.FindByIdAndTeamId(taskId, teamId))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Models.Task>(new Domain.Models.Task { Description = description}));
            var mockTeamRepository = GetDefaultITeamRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object);

            //Act
            var result = await service.GetByIdAndTeamIdAsync(taskId, teamId);
            var task = result.Resource;

            //Assert
            task.Description.Should().Be("Clean the bathroom");
        }

        [Test]
        public async System.Threading.Tasks.Task FinishTaskWhenTaskIsAlreadyFinishReturnsErrorMessage()
        {
            //Arrange
            var taskId = 1;
            var teamId = 1;
            var description = "Clean the livingroom";

            var mockTaskRepository = GetDefaultITaskRepositoryInstance();
            mockTaskRepository.Setup(t => t.FindByIdAndTeamId(taskId, teamId))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Models.Task>(new Domain.Models.Task { Description = description, Active = false }));
            var mockTeamRepository = GetDefaultITeamRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object);

            //Act
            var result = await service.FinishAsync(taskId, teamId);
            var message = result.Message;

            //Assert
            message.Should().Be("Task is already finish");
        }

        [Test]
        public async System.Threading.Tasks.Task FinishTaskWhenTaskIsActiveReturnsTaskWithActiveEqualsFalse()
        {
            //Arrange
            var taskId = 1;
            var teamId = 1;
            var description = "Clean the livingroom";

            var mockTaskRepository = GetDefaultITaskRepositoryInstance();
            mockTaskRepository.Setup(t => t.FindByIdAndTeamId(taskId, teamId))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Models.Task>(new Domain.Models.Task { Description = description, Active = true }));
            var mockTeamRepository = GetDefaultITeamRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object, mockTeamRepository.Object);

            //Act
            var result = await service.FinishAsync(taskId, teamId);
            var resource = result.Resource;

            //Assert
            resource.Active.Should().Be(false);
        }

        private Mock<ITaskRepository> GetDefaultITaskRepositoryInstance()
        {
            return new Mock<ITaskRepository>();
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

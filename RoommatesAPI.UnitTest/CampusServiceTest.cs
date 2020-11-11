using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Services;

namespace Roommates.API.Test
{
    public class CampusServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task GetByIdAsynWhenIdNoExistsReturnsNotFoundException()
        {
            //Arrange
            var mockCampusRepository = GetDefaultICampusRepositoryInstance();
            int campusId = 1;
            int id = 1;
            mockCampusRepository.Setup(c => c.FindByIdAndStudyCenterId(id,campusId))
                .Returns(System.Threading.Tasks.Task.FromResult<Campus>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockStudyCenterRepository = GetDefaultIStudyCenterRepositoryInstance();
            mockStudyCenterRepository.Setup(c => c.FindById(id))
                .Returns(System.Threading.Tasks.Task.FromResult<StudyCenter>(null));

            var service = new CampusService(mockCampusRepository.Object, mockUnitOfWork.Object,mockStudyCenterRepository.Object);

            //Act
            CampusResponse result = await service.GetByIdAndStudyCenterIdAsync(id,campusId);
            var message = result.Message;


            //Assert
            message.Should().Be("Campus not found");
        }

        private Mock<ICampusRepository> GetDefaultICampusRepositoryInstance()
        {
            return new Mock<ICampusRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IStudyCenterRepository> GetDefaultIStudyCenterRepositoryInstance()
        {
            return new Mock<IStudyCenterRepository>();
        }
    }
}


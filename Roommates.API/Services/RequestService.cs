using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILessorRepository _lessorRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository friendshipRequestRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork, ILessorRepository lessorRepository, IPropertyRepository propertyRepository)
        {
            _requestRepository = friendshipRequestRepository;
            _studentRepository = studentRepository;
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
            _propertyRepository = propertyRepository;
        }

        public async Task<RequestResponse> AddTeamRequestAsync(int personOneId, int personTwoId)
        {
            if (personOneId == personTwoId)
                return new RequestResponse("You can not send a friend request to yourself");

            var studentOne = await _studentRepository.FindById(personOneId);
            var studentTwo = await _studentRepository.FindById(personTwoId);

            if (!studentOne.Available)
                return new RequestResponse("You can not send a friend request because you have roommate");

            if (!studentTwo.Available)
                return new RequestResponse("The student you want to send a friend request to, have roommate ");

            var newFriendshipRequest = new Request
            {
                PersonOneId = studentOne.Id,
                PersonOne = studentOne,
                PersonTwoId = studentTwo.Id,
                PersonTwo = studentTwo,
                StatusDetail = "Pending",
                Type = "teamrequest"
            };

            try
            {
                await _requestRepository.AddAsync(newFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new RequestResponse(newFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new RequestResponse($"An error ocurred while adding a team request :{ex.Message}");
            }
        }

        public async Task<RequestResponse> AddLeaseRequestAsync(int personOneId, int personTwoId, int propertyId)
        {
            var existingProperty = await _propertyRepository.FindById(propertyId);
            if (existingProperty == null)
                return new RequestResponse("Property not found");

            if (personOneId == personTwoId)
                return new RequestResponse("You can not send a friend request to yourself");

            var lessor = await _lessorRepository.FindById(personTwoId);

            if(lessor == null)
                return new RequestResponse("The lessor is not found or is not avaible at this moment");
            
            var student = await _studentRepository.FindById(personOneId);

            var newFriendshipRequest = new Request
            {
                PersonOneId = student.Id,
                PersonOne = student,
                PersonTwoId = lessor.Id,
                PersonTwo = lessor,
                StatusDetail = "Pending",
                Type = "lessorrequest",
                PropertyId = propertyId
            };

            try
            {
                await _requestRepository.AddAsync(newFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new RequestResponse(newFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new RequestResponse($"An error ocurred while adding a lessor request :{ex.Message}");
            }
        }

        public async Task<RequestResponse> AnswerRequest(int personOneId, int id, int answer)
        {
            var statusDetail = string.Empty;

            if (answer != 1 && answer != 2)
                return new RequestResponse("You must select a valid answer");

            if (answer == 1)
                statusDetail = "Accepted";
            else
                statusDetail = "Declined";

            var existingFriendshipRequest = await _requestRepository.FindByPersonOneIdAndPersonTwoId(personOneId, id);

            if (existingFriendshipRequest == null)
                return new RequestResponse("Request does not exist");

            existingFriendshipRequest.Status = answer;
            existingFriendshipRequest.StatusDetail = statusDetail;

            try
            {
                _requestRepository.Update(existingFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new RequestResponse(existingFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new RequestResponse($"An error ocurred while updating a  request :{ex.Message}");
            }

        }

        public async Task<RequestResponse> GetByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId)
        {
            var existingFriendshipRequest = await _requestRepository.FindByPersonOneIdAndPersonTwoId(personOneId, personTwoId);

            if (existingFriendshipRequest == null)
                return new RequestResponse("Request does not exist");

            return new RequestResponse(existingFriendshipRequest);
        }

        public async Task<IEnumerable<Request>> GetReceivedRequests(int personTwoId)
        {
            return await _requestRepository.ListByPersonTwoIdAsync(personTwoId);
        }

        public async Task<IEnumerable<Request>> GetSentRequests(int personOneId)
        {
            return await _requestRepository.ListByPersonOneIdAsync(personOneId);
        }

        public async Task<RequestResponse> DeleteAsync(int personOneId, int personTwoId)
        {

            var existingFriendshipRequest = await _requestRepository.FindByPersonOneIdAndPersonTwoId(personOneId, personTwoId);

            if (existingFriendshipRequest == null)
                return new RequestResponse("Request does not exist");

            try
            {
                _requestRepository.Remove(existingFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new RequestResponse(existingFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new RequestResponse($"An error ocurred while updating a  request :{ex.Message}");
            }
        }
    }
}

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
        private readonly IRequestRepository _friendshipRequestRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ILessorRepository _lessorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository friendshipRequestRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork, IPersonRepository personRepository, ILessorRepository lessorRepository)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _studentRepository = studentRepository;
            _personRepository = personRepository;
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendshipRequestResponse> AddTeamRequestAsync(int personOneId, int personTwoId)
        {
            if (personOneId == personTwoId)
                return new FriendshipRequestResponse("You can not send a friend request to yourself");

            var studentOne = await _studentRepository.FindById(personOneId);
            var studentTwo = await _studentRepository.FindById(personTwoId);

            if (!studentOne.Available)
                return new FriendshipRequestResponse("You can not send a friend request because you have roommate");

            if (!studentTwo.Available)
                return new FriendshipRequestResponse("The student you want to send a friend request to, have roommate ");

            var newFriendshipRequest = new FriendshipRequest
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
                await _friendshipRequestRepository.AddAsync(newFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new FriendshipRequestResponse(newFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new FriendshipRequestResponse($"An error ocurred while adding a team request :{ex.Message}");
            }
        }

        public async Task<FriendshipRequestResponse> AddLessorRequestAsync(int personOneId, int personTwoId)
        {
            if (personOneId == personTwoId)
                return new FriendshipRequestResponse("You can not send a friend request to yourself");

            var lessor = await _lessorRepository.FindById(personTwoId);
            if(lessor == null)
                return new FriendshipRequestResponse("The lessor is not found or is not avaible at this moment");
            
            var student = await _studentRepository.FindById(personOneId);

            var newFriendshipRequest = new FriendshipRequest
            {
                PersonOneId = student.Id,
                PersonOne = student,
                PersonTwoId = lessor.Id,
                PersonTwo = lessor,
                StatusDetail = "Pending",
                Type = "lessorrequest"
            };
            try
            {
                await _friendshipRequestRepository.AddAsync(newFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new FriendshipRequestResponse(newFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new FriendshipRequestResponse($"An error ocurred while adding a lessor request :{ex.Message}");
            }
        }

        public async Task<FriendshipRequestResponse> AnswerRequest(int personOneId, int id, int answer)
        {
            var statusDetail = string.Empty;

            if (answer != 1 && answer != 2)
                return new FriendshipRequestResponse("You must select a valid answer");

            if (answer == 1)
                statusDetail = "Accepted";
            else
                statusDetail = "Declined";

            var existingFriendshipRequest = await _friendshipRequestRepository.FindByPersonOneIdAndPersonTwoId(personOneId, id);

            if (existingFriendshipRequest == null)
                return new FriendshipRequestResponse("Friendship Request does not exist");

            existingFriendshipRequest.Status = answer;
            existingFriendshipRequest.StatusDetail = statusDetail;

            try
            {
                _friendshipRequestRepository.Update(existingFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new FriendshipRequestResponse(existingFriendshipRequest);

            }
            catch (Exception ex)
            {
                return new FriendshipRequestResponse($"An error ocurred while updating a  request :{ex.Message}");
            }

        }

        public async Task<FriendshipRequestResponse> GetByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId)
        {
            var existingFriendshipRequest = await _friendshipRequestRepository.FindByPersonOneIdAndPersonTwoId(personOneId, personTwoId);

            if (existingFriendshipRequest == null)
                return new FriendshipRequestResponse("Request does not exist");

            return new FriendshipRequestResponse(existingFriendshipRequest);
        }

        public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestReceive(int personTwoId)
        {
            return await _friendshipRequestRepository.ListByPersonTwoIdAsync(personTwoId);
        }

        public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestSent(int personOneId)
        {
            return await _friendshipRequestRepository.ListByPersonOneIdAsync(personOneId);
        }
    }
}

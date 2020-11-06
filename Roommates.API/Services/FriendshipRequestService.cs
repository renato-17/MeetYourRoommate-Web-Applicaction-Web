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
    public class FriendshipRequestService : IFriendshipRequestService
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FriendshipRequestService(IFriendshipRequestRepository friendshipRequestRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendshipRequestResponse> AddAsync(int studentOneId, int studentTwoId)
        {
            if (studentOneId == studentTwoId)
                return new FriendshipRequestResponse("You can not send a friend request to yourself");

            var studentOne = await _studentRepository.FindById(studentOneId);
            var studentTwo = await _studentRepository.FindById(studentTwoId);

            if (!studentOne.Available)
                return new FriendshipRequestResponse("You can not send a friend request because you have roommate");

            if (!studentTwo.Available)
                return new FriendshipRequestResponse("The student you want to send a friend request to, have roommate ");

            var newFriendshipRequest = new FriendshipRequest
            {
                StudentOneId = studentOneId,
                StudentOne = studentOne,
                StudentTwoId = studentTwoId,
                StudentTwo = studentTwo,
                StatusDetail = "Pending"
            };

            try
            {
                await _friendshipRequestRepository.AddAsync(newFriendshipRequest);
                await _unitOfWork.CompleteAsync();

                return new FriendshipRequestResponse(newFriendshipRequest);

            }catch(Exception ex)
            {
                return new FriendshipRequestResponse($"An error ocurred while adding a friendship request :{ex.Message}");
            }

        }

        public async Task<FriendshipRequestResponse> AnswerRequest(int studentOneId, int id, int answer)
        {
            var statusDetail = string.Empty;

            if (answer != 1 && answer != 2)
                return new FriendshipRequestResponse("You must select a valid answer");

            if (answer == 1)
                statusDetail = "Accepted";
            else
                statusDetail = "Declined";

            var existingFriendshipRequest = await _friendshipRequestRepository.FindByStudentOneIdAndStudentTwoId(studentOneId, id);

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
                return new FriendshipRequestResponse($"An error ocurred while updating a friendship request :{ex.Message}");
            }

        }

        public async Task<FriendshipRequestResponse> GetByStudentOneIdAndStudentTwoId(int studentOneId, int studentTwoId)
        {
            var existingFriendshipRequest = await _friendshipRequestRepository.FindByStudentOneIdAndStudentTwoId(studentOneId, studentTwoId);

            if (existingFriendshipRequest == null)
                return new FriendshipRequestResponse("Friendship Request does not exist");

            return new FriendshipRequestResponse(existingFriendshipRequest);
        }

        public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestReceive(int studentTwoId)
        {
            return await _friendshipRequestRepository.ListByStudentTwoIdAsync(studentTwoId);
        }

        public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestSent(int studentOneId)
        {
            return await _friendshipRequestRepository.ListByStudentOneIdAsync(studentOneId);
        }
    }
}

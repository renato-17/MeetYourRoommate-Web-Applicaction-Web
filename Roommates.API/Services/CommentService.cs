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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAdRepository _adRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository commentRepository, IPersonRepository personRepository, IAdRepository adRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _personRepository = personRepository;
            _adRepository = adRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentResponse> DeleteAsync(int id, int adId)
        {
            var existingComment = await _commentRepository.FindByIdAndAdId(id, adId);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            try
            {
                _commentRepository.Remove(existingComment);
                await _unitOfWork.CompleteAsync();
                return new CommentResponse(existingComment);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while deleting Comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> FindById(int id)
        {
            var existingComment = await _commentRepository.FindById(id);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            return new CommentResponse(existingComment);
        }

        public async Task<CommentResponse> FindByIdAndAdId(int id, int adId)
        {
            var existingComment = await _commentRepository.FindByIdAndAdId(id, adId);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            return new CommentResponse(existingComment);
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _commentRepository.ListAsync();
        }

        public async Task<IEnumerable<Comment>> ListByAdIdAsync(int adId)
        {
            return await _commentRepository.ListByAdIdAsync(adId);
        }

        public async Task<IEnumerable<Comment>> ListByPersonIdAsync(int personId)
        {
            return await _commentRepository.ListByPersonIdAsync(personId);
        }

        public async Task<CommentResponse> SaveAsync(Comment comment, int adId)
        {
            var existingAd = await _adRepository.FindById(adId);
            if(existingAd == null)
                return new CommentResponse("Ad not found");
            var existingPerson = await _personRepository.FindById(comment.PersonId);
            if (existingPerson == null)
                return new CommentResponse("Person not found");
            comment.Ad = existingAd;
            comment.AdId = existingAd.Id;
            comment.Person = existingPerson;
            try
            {
                await _commentRepository.AddAsync(comment);
                await _unitOfWork.CompleteAsync();
                return new CommentResponse(comment);
            }
            catch(Exception ex)
            {
                return new CommentResponse($"An error ocurred while saving comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> UpdateAsync(Comment comment, int id, int adId)
        {
            var existingAd = await _adRepository.FindById(adId);
            if (existingAd == null)
                return new CommentResponse("Ad not found");
            var existingComment = await _commentRepository.FindByIdAndAdId(id, adId);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            existingComment.Description = comment.Description;
            existingComment.DateUpdated = comment.DateCreated;
            try
            {
                _commentRepository.Update(existingComment);
                await _unitOfWork.CompleteAsync();
                return new CommentResponse(existingComment);
            }
            catch(Exception ex)
            {
                return new CommentResponse($"An error ocurred while updating comment: {ex.Message}");
            }
        }
    }
}

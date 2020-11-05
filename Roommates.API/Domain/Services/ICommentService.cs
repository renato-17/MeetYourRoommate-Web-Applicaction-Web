using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> ListAsync();
        Task<IEnumerable<Comment>> ListByAdIdAsync(int adId);
        Task<IEnumerable<Comment>> ListByPersonIdAsync(int personId);
        Task<CommentResponse> FindByIdAndAdId(int id, int adId);
        Task<CommentResponse> FindById(int id);
        Task<CommentResponse> SaveAsync(Comment comment, int adId);
        Task<CommentResponse> UpdateAsync(Comment comment, int id, int adId);
        Task<CommentResponse> DeleteAsync(int id, int adId);
    }
}

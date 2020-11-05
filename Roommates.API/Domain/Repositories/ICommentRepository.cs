using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> ListAsync();
        Task<IEnumerable<Comment>> ListByAdIdAsync(int adId);
        Task<IEnumerable<Comment>> ListByPersonIdAsync(int personId);
        Task<Comment> FindByIdAndAdId(int id,int adId);
        Task<Comment> FindById(int id);
        System.Threading.Tasks.Task AddAsync(Comment comment);
        void Update(Comment comment);
        void Remove(Comment comment);
    }
}

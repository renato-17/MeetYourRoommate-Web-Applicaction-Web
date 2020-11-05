using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Persistance.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<Comment> FindByIdAndAdId(int id, int adId)
        {
            return await _context.Comments
                .SingleAsync(c => (c.Id == id) && (c.AdId == adId));
        }

        public async Task<IEnumerable<Comment>> ListByPersonIdAsync(int personId)
        {
            return await _context.Comments
                .Where(c => c.PersonId == personId)
                .Include(c => c.Person)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> ListByAdIdAsync(int adId)
        {
            return await _context.Comments
                .Where(c => c.AdId == adId)
                .Include(c => c.Ad)
                .ToListAsync();
        }

        public void Remove(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> FindById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}

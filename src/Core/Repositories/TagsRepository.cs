using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly CoreDbContext dbContext;
        private readonly CoreUnitOfWork unitOfWork;

        public TagsRepository(CoreDbContext dbContext, CoreUnitOfWork unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Tag tag)
        {
            dbContext.Tags.Add(tag);
        }

        public void Update(Tag tag)
        {
            dbContext.Tags.Update(tag);
        }

        public void Remove(Tag tag)
        {
            dbContext.Tags.Remove(tag);
        }

        public Task<List<Tag>> AllAsync()
        {
            return dbContext.Tags
                .ToListAsync();
        }

        public Task<Tag> FindAsync(Guid tagId)
        {
            return dbContext.Tags
                .FirstOrDefaultAsync(t => t.Id == tagId);
        }
    }
}
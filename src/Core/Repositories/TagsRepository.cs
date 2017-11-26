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

        public Task<List<Tag>> AllAsync()
        {
            return dbContext.Tags
                .ToListAsync();
        }

        public Task<Tag> FindAsync(string name)
        {
            return dbContext.Tags
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
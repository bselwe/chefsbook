
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Services
{
    public class TagsService : ITagsService
    {
        private ITagsRepository tagsRepository;
        private CoreDbContext dbContext;
        private CoreUnitOfWork unitOfWork;

        public TagsService(
            ITagsRepository tagsRepository,
            CoreDbContext dbContext,
            CoreUnitOfWork unitOfWork)
        {
            this.tagsRepository = tagsRepository;
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
        }

        public Task<List<Tag>> AllAsync()
        {
            return tagsRepository.AllAsync();
        }

        public Task<Tag> FindAsync(string name)
        {
            return tagsRepository.FindAsync(name);
        }
    }
}
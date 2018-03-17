
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
        private readonly ITagsRepository tagsRepository;

        public TagsService(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
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
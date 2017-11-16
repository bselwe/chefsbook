using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public interface ITagsRepository
    {
        void Add(Tag tag);
        void Update(Tag tag);
        void Remove(Tag tag);
        Task<List<Tag>> AllAsync();
        Task<Tag> FindAsync(Guid tagId);
    }
}
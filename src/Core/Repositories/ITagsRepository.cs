using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public interface ITagsRepository
    {
        Task<List<Tag>> AllAsync();
        Task<Tag> FindAsync(string name);
    }
}
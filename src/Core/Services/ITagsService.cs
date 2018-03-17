using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Services
{
    public interface ITagsService
    {
        Task<List<Tag>> AllAsync();
        Task<Tag> FindAsync(string name);
    }
}
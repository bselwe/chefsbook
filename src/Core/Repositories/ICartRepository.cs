using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Repositories
{
    public interface ICartRepository
    {
        void Add(CartItem item);
        void RemoveAll(Guid userId);
        Task<List<CartItem>> AllAsync(Guid userId);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Models;

namespace ChefsBook.Core.Services
{
    public interface ICartService
    {
        Task AddAsync(CartItem item);
        Task RemoveAllAsync(Guid userId);
        Task<List<CartItem>> AllAsync(Guid userId);
    }
}
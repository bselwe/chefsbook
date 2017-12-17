
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CoreDbContext dbContext;

        public CartRepository(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(CartItem item)
        {
            dbContext.CartItems.Add(item);
        }

        public void RemoveAll(Guid userId)
        {
            var items = dbContext.CartItems
                .Where(i => i.UserId == userId);
            dbContext.CartItems.RemoveRange(items);
        }

        public Task<List<CartItem>> AllAsync(Guid userId)
        {
            return dbContext.CartItems
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }
    }
}
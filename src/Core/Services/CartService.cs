
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly CoreUnitOfWork unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            CoreUnitOfWork unitOfWork)
        {
            this.cartRepository = cartRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CartItem item)
        {
            cartRepository.Add(item);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveAllAsync(Guid userId)
        {
            cartRepository.RemoveAll(userId);
            await unitOfWork.CommitAsync();
        }

        public Task<List<CartItem>> AllAsync(Guid userId)
        {
            return cartRepository.AllAsync(userId);
        }
    }
}
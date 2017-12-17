
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Auth.Security;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Models;
using ChefsBook.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChefsBook.WebApiApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartController(
            ICartService cartService,
            IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }

        [HttpPost("{recipeId}")]
        [SwaggerResponse((int) HttpStatusCode.Created)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddToCart(Guid recipeId)
        {
            if (recipeId == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var cartItem = CartItem.Create(userId, recipeId);
            await cartService.AddAsync(cartItem);

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<TagDTO>))]
        public async Task<IActionResult> GetCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var cart = await cartService.AllAsync(userId);
            var mappedCart = mapper.Map<List<CartItemDTO>>(cart);
            return Ok(mappedCart);
        }

        [HttpDelete]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            await cartService.RemoveAllAsync(userId);
            return Ok();
        }
    }
}

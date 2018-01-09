
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRecipesService recipesService;
        private readonly IMapper mapper;

        public CartController(
            ICartService cartService,
            IRecipesService recipesService,
            IMapper mapper)
        {
            this.cartService = cartService;
            this.recipesService = recipesService;
            this.mapper = mapper;
        }

        [HttpPost("{recipeId}")]
        [SwaggerResponse((int) HttpStatusCode.Created)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddRecipeToCart(Guid recipeId)
        {
            if (recipeId == null)
                return BadRequest();

            var recipe = await recipesService.FindAsync(recipeId);
            if (recipe == null)
                return NotFound();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var cartItem = CartItem.Create(userId, recipe);
            await cartService.AddAsync(cartItem);

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<IngredientDTO>))]
        public async Task<IActionResult> GetCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var cart = await cartService.AllAsync(userId);
            var ingredients = cart.Select(c => c.Recipe.Ingredients);
            var mappedIngredients = new List<List<IngredientDTO>>();

            foreach (var recipeIngredients in ingredients)
            {
                mappedIngredients.Add(mapper.Map<List<IngredientDTO>>(recipeIngredients));
            }

            return Ok(mappedIngredients);
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

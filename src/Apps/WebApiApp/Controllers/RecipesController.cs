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
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly IMapper mapper;

        public RecipesController(
            IRecipesService recipesService,
            IMapper mapper)
        {
            this.recipesService = recipesService;
            this.mapper = mapper;
        }

        [HttpGet("me")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<RecipeDTO>))]
        public async Task<IActionResult> GetUserRecipes()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));

            if (userId == null)
                return NotFound();

            var recipes = await recipesService.AllByUserAsync(userId);
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpGet("public/{userId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<RecipeDTO>))]
        public async Task<IActionResult> GetUserPublicRecipes(Guid userId)
        {
            if (userId == null)
                return NotFound();

            var recipes = await recipesService.AllPublicByUserAsync(userId);
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(RecipeDetailsDTO))]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var recipe = await recipesService.FindAsync(id);

            if (recipe == null || (recipe.IsPrivate && recipe.UserId != userId))
                return NotFound();
            
            var mappedRecipe = mapper.Map<RecipeDetailsDTO>(recipe);
            return Ok(mappedRecipe);
        }

        [HttpPost("filter")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<RecipeDTO>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FilterRecipes([FromBody] FilterRecipeDTO filter)
        {
            var recipes = new List<Recipe>();

            if (string.IsNullOrEmpty(filter.Text) && (filter.Tags == null || filter.Tags.Count == 0))
                recipes = await recipesService.AllPublicAsync();
            else
                recipes = await recipesService.FilterAsync(filter.Text, filter.Tags);
            
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.Created)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRecipe([FromBody] NewRecipeDTO recipe)
        {
            if (recipe == null || recipe.Ingredients == null || recipe.Steps == null || recipe.Tags == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => Tag.Create(t.Name)).ToList();
            
            await recipesService.CreateAsync(
                userId,
                recipe.Title,
                recipe.IsPrivate,
                recipe.Description,
                recipe.Image,
                recipe.Duration,
                recipe.Servings,
                recipe.Notes,
                ingredients,
                steps,
                tags
            );

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] UpdateRecipeDTO recipe)
        {
            if (recipe == null || recipe.Ingredients == null || recipe.Steps == null || recipe.Tags == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => Tag.Create(t.Name)).ToList();

            var result = await recipesService.UpdateAsync(
                id,
                userId,
                recipe.Title,
                recipe.IsPrivate,
                recipe.Description,
                recipe.Image,
                recipe.Duration,
                recipe.Servings,
                recipe.Notes,
                ingredients,
                steps,
                tags
            );

            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var result = await recipesService.RemoveAsync(id, userId);

            if (!result)
                return NotFound();
            return Ok();
        }
    }
}

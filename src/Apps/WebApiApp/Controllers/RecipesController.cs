using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Core;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Models;
using ChefsBook.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChefsBook.WebApiApp.Controllers
{
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

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<RecipeDTO>))]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await recipesService.AllAsync();
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(RecipeDetailsDTO))]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await recipesService.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            var mappedRecipe = mapper.Map<RecipeDetailsDTO>(recipe);
            return Ok(mappedRecipe);
        }

        [HttpPost("filter")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<RecipeDTO>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FilterRecipes([FromBody] FilterRecipeDTO filter)
        {
            if (string.IsNullOrEmpty(filter.Text) && filter.Tags == null)
                return BadRequest();

            var recipes = await recipesService.FilterAsync(filter.Text, filter.Tags);
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

            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => Tag.Create(t.Name)).ToList();
            
            await recipesService.CreateAsync(
                recipe.Title,
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

            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => Tag.Create(t.Name)).ToList();

            var result = await recipesService.UpdateAsync(
                id,
                recipe.Title,
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
            var result = await recipesService.RemoveAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}

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
using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers
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
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await recipesService.AllAsync();
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await recipesService.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            var mappedRecipe = mapper.Map<RecipeDetailsDTO>(recipe);
            return Ok(mappedRecipe);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterRecipes([FromBody] FilterRecipeDTO filter)
        {
            if (string.IsNullOrEmpty(filter.Text) && filter.Tags == null)
                return BadRequest();

            var recipes = await recipesService.FilterAsync(filter.Text, filter.Tags);
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] NewRecipeDTO recipe)
        {
            if (recipe == null)
                return BadRequest();

            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => t.Id.HasValue ? Tag.Create((Guid) t.Id, t.Name) : Tag.Create(t.Name)).ToList();
            
            await recipesService.Create(
                recipe.Title,
                recipe.Description,
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
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] UpdateRecipeDTO recipe)
        {
            if (recipe == null)
                return BadRequest();

            var ingredients = recipe.Ingredients.Select(i => Ingredient.Create(i.Name, i.Quantity)).ToList();
            var steps = recipe.Steps.Select(s => Step.Create(s.Duration, s.Description)).ToList();
            var tags = recipe.Tags.Select(t => t.Id.HasValue ? Tag.Create((Guid) t.Id, t.Name) : Tag.Create(t.Name)).ToList();

            var result = await recipesService.Update(
                id,
                recipe.Title,
                recipe.Description,
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
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var result = await recipesService.Remove(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Core;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;
using Core.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipesRepository recipesRepository;
        private readonly CoreUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RecipesController(
            IRecipesRepository recipesRepository, 
            CoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.recipesRepository = recipesRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await recipesRepository.AllAsync();
            var mappedRecipes = mapper.Map<List<RecipeDTO>>(recipes);
            return Ok(mappedRecipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            var mappedRecipe = mapper.Map<RecipeDetailsDTO>(recipe);
            return Ok(mappedRecipe);
        }

        [HttpPost]
        public async Task<IActionResult> NewRecipe([FromBody] NewRecipeDTO newRecipe)
        {
            if (newRecipe == null)
                return BadRequest();

            var recipe = Recipe.Create(
                newRecipe.Title, 
                newRecipe.Description, 
                newRecipe.Duration, 
                newRecipe.Servings,
                newRecipe.Notes);

            var ingredients = newRecipe.Ingredients.Select(i => Ingredient.Create(recipe, i.Name, i.Quantity)).ToList();
            var steps = newRecipe.Steps.Select(s => Step.Create(recipe, s.Duration, s.Description)).ToList();
            recipe.AddIngredients(ingredients);
            recipe.AddSteps(steps);

            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] UpdateRecipeDTO updateRecipe)
        {
            if (updateRecipe == null)
                return BadRequest();

            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();

            recipe.Update(
                updateRecipe.Title, 
                updateRecipe.Description, 
                updateRecipe.Duration, 
                updateRecipe.Servings,
                updateRecipe.Notes);

            var ingredients = updateRecipe.Ingredients.Select(i => Ingredient.Create(recipe, i.Name, i.Quantity)).ToList();
            var steps = updateRecipe.Steps.Select(s => Step.Create(recipe, s.Duration, s.Description)).ToList();
            recipe.UpdateIngredients(ingredients);
            recipe.UpdateSteps(steps);

            recipesRepository.Update(recipe);
            await unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            recipesRepository.Remove(recipe);
            await unitOfWork.CommitAsync();
            
            return Ok();
        }
    }
}

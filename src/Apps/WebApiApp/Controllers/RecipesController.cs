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
using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;

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
            
            var mappedRecipe = mapper.Map<RecipeDTO>(recipe);
            return Ok(mappedRecipe);
        }

        [HttpPost]
        public async Task<IActionResult> NewRecipe([FromBody] NewRecipeParams newRecipe)
        {
            if (newRecipe == null)
                return BadRequest();

            var recipe = Recipe.Create(
                newRecipe.Title, 
                newRecipe.Description, 
                newRecipe.Duration, 
                newRecipe.Servings,
                newRecipe.Notes);

            var ingredients = new List<Ingredient>();
            foreach (var i in newRecipe.Ingredients)
            {
                var ingredient = Ingredient.Create(recipe, i.Name, i.Quantity);
                ingredients.Add(ingredient);
            }

            var steps = new List<Step>();
            foreach (var s in newRecipe.Steps)
            {
                var step = Step.Create(recipe, s.Duration, s.Description);
                steps.Add(step);
            }

            recipe.AddIngredients(ingredients);
            recipe.AddSteps(steps);
            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            recipesRepository.Remove(recipe);
            await unitOfWork.CommitAsync();
            return NoContent();
        }
    }
}

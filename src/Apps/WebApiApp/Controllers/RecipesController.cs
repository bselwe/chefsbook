using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChefsBook.Core;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.ControllersParams;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipesRepository recipesRepository;
        private readonly CoreUnitOfWork unitOfWork;

        public RecipesController(IRecipesRepository recipesRepository, CoreUnitOfWork unitOfWork)
        {
            this.recipesRepository = recipesRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await recipesRepository.AllAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromForm] RecipeParams @params)
        {
            if (@params == null)
                return BadRequest();

            var recipe = Recipe.Create(@params.Title, @params.Description);
            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }
    }
}

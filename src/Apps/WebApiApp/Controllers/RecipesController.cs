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
using WebApiApp.ControllersParams;

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
        public async Task<IActionResult> CreateRecipe([FromForm] RecipeParams @params)
        {
            if (@params == null)
                return BadRequest();

            var recipe = Recipe.Create(@params.Title, @params.Description, @params.Duration, @params.Servings, @params.Notes);
            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromForm] RecipeParams @params) 
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return NotFound();
            
            recipesRepository.Update(recipe);
            await unitOfWork.CommitAsync();
            return NoContent();
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

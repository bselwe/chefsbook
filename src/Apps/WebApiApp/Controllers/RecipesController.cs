using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChefsBook.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.ControllersParams;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private IRecipesRepository recipesRepository;

        public RecipesController(IRecipesRepository recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }

        [HttpGet]
        public IActionResult GetRecipes()
        {
            var recipes = recipesRepository.AllAsync().Result;
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipeById(Guid id)
        {
            var recipe = recipesRepository.FindAsync(id).Result;
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }
    }
}

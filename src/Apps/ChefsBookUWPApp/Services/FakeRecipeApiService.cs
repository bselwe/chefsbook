using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;

namespace ChefsBook_UWP_App.Services
{
    public class FakeRecipeApiService : IRecipeApiService
    {
        private List<RecipeDTO> _recipes = new List<RecipeDTO>();

        public FakeRecipeApiService()
        {
            _recipes = new List<RecipeDTO>()
            {
                new RecipeDTO
                {
                    Id = Guid.NewGuid(),
                    Title = "Moule the crema with oreiv lemoinaie",
                    Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Image = @"/Assets/seafood_dish.jpg",
                    Duration = TimeSpan.FromMinutes(15),
                    Servings = 4,
                    Notes = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. "
                },
                new RecipeDTO
                {
                    Id = Guid.NewGuid(),
                    Title = "Bacon Cheese Spread with Carmelized Onions",
                    Description = @"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Image = @"/Assets/seafood_dish.jpg",
                    Notes = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. "
                }
            };
        }

        public Task<List<RecipeDTO>> GetAllRecipes()
        {
            return Task.FromResult(_recipes);
        }

        public Task<RecipeDTO> GetRecipe(Guid id)
        {
            var foundRecipe = _recipes.Find(r => r.Id == id);
            return Task.FromResult(foundRecipe);
        }

        public Task AddRecipe(RecipeDTO recipe)
        {
            _recipes.Add(recipe);
            return Task.CompletedTask;
        }

        public Task EditRecipe(RecipeDTO recipe)
        {
            var oldRecipeIndex = _recipes.FindIndex(r => r.Id == recipe.Id);
            _recipes[oldRecipeIndex] = recipe;
            return Task.CompletedTask;
        }

        public Task DeleteRecipe(RecipeDTO recipe)
        {
            _recipes.Remove(GetRecipe(recipe.Id).Result);
            return Task.CompletedTask;
        }
    }
}

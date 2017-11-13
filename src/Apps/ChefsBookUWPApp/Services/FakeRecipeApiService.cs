﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefsBook.Core.Contracts;
using Core.Contracts;
using System.Linq;

namespace ChefsBook_UWP_App.Services
{
    public class FakeRecipeApiService : IRecipeApiService
    {
        private List<RecipeDetailsDTO> _recipes = new List<RecipeDetailsDTO>();
        private List<TagDTO> _tags = new List<TagDTO>();

        public FakeRecipeApiService()
        {
            _tags = new List<TagDTO>()
            {
                new TagDTO()
                {
                    Id = new Guid("f69aeab4-761b-43e8-adb9-b5f5c0b994cb"),
                    Name = "breakfast"
                },
                new TagDTO()
                {
                    Id = new Guid("063e51be-9875-441b-a486-338c8e177a68"),
                    Name = "protein-rich"
                }
            };

            _recipes = new List<RecipeDetailsDTO>()
            {
                new RecipeDetailsDTO
                {
                    Id = new Guid("4b602f03-5da9-4450-9946-6b248d26b142"),
                    Title = "Moule the crema with oreiv lemoinaie",
                    Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Image = @"/Assets/seafood_dish.jpg",
                    Duration = TimeSpan.FromMinutes(15),
                    Servings = 4,
                    Tags = _tags,
                    Ingredients = new List<IngredientDTO>()
                    {
                        new IngredientDTO()
                        {
                            Name = "Milk",
                            Quantity = "1 liter"
                        },
                        new IngredientDTO()
                        {
                            Name = "Pasta",
                            Quantity = "1 kg"
                        }
                    },
                    Steps = new List<StepDTO>()
                    {
                        new StepDTO()
                        {
                            Duration = TimeSpan.FromMinutes(15),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit"
                        },
                        new StepDTO()
                        {
                            Duration = TimeSpan.FromMinutes(80),
                            Description = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt."
                        }
                    },
                    Notes = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt."
                },
                new RecipeDetailsDTO
                {
                    Id = new Guid("d12b42f7-2348-481e-9dad-9392212aaaa3"),
                    Title = "Bacon Cheese Spread with Carmelized Onions",
                    Description = @"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Image = @"/Assets/seafood_dish.jpg",
                    Tags = new List<TagDTO>(),
                    Ingredients = new List<IngredientDTO>(),
                    Steps = new List<StepDTO>(),
                    Notes = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt."
                }
            };
        }

        public Task<List<RecipeDTO>> GetAllRecipes()
        {
            return Task.FromResult(new List<RecipeDTO>(_recipes.ConvertAll(r => (RecipeDTO)r)));
        }

        public Task<RecipeDetailsDTO> GetRecipe(Guid id)
        {
            var foundRecipe = _recipes.Find(r => r.Id == id);
            return Task.FromResult(foundRecipe);
        }

        public Task AddRecipe(RecipeDetailsDTO recipe)
        {
            _recipes.Add(recipe);
            return Task.CompletedTask;
        }

        public Task EditRecipe(RecipeDetailsDTO recipe)
        {
            var oldRecipeIndex = _recipes.FindIndex(r => r.Id == recipe.Id);
            _recipes[oldRecipeIndex] = recipe;
            return Task.CompletedTask;
        }

        public Task DeleteRecipe(RecipeDetailsDTO recipe)
        {
            _recipes.Remove(GetRecipe(recipe.Id).Result);
            return Task.CompletedTask;
        }

        public Task<List<TagDTO>> GetAllTags()
        {
            return Task.FromResult(_tags);
        }

        public Task<List<RecipeDetailsDTO>> FilterRecipes(FilterRecipeDTO filter)
        {
            var results = _recipes.Where(r =>
                    ((filter.Text == null || filter.Text.Length == 0 ||
                    r.Title.ToLower().Contains(filter.Text.ToLower()) ||
                    r.Description.ToLower().Contains(filter.Text.ToLower())) &&
                    (filter.Tags == null || filter.Tags.Count == 0 ||
                    r.Tags.Select(t => t.Id).Intersect(filter.Tags).Any()))).ToList();

            return Task.FromResult(results);
        }
    }
}

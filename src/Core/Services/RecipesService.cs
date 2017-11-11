using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;

namespace ChefsBook.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private IRecipesRepository recipesRepository;
        private ITagsRepository tagsRepository;
        private CoreUnitOfWork unitOfWork;

        public RecipesService(
            IRecipesRepository recipesRepository, 
            ITagsRepository tagsRepository,
            CoreUnitOfWork unitOfWork)
        {
            this.recipesRepository = recipesRepository;
            this.tagsRepository = tagsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Create(
            string title, string description, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags)
        {
            var recipe = Recipe.Create(title, description, duration, servings, notes, ingredients, steps);
            var recipeTags = await CreateTagsForRecipe(recipe, tags);
            recipe.AddTags(recipeTags);

            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();
        }

        public async Task<bool> Update(
            Guid id, string title, string description, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags)
        {
            var recipe = await recipesRepository.FindAsync(id);
            if (recipe == null)
                return false;

            recipe.Update(title, description, duration, servings, notes, ingredients, steps);
            var recipeTags = await CreateTagsForRecipe(recipe, tags);
            recipe.UpdateTags(recipeTags);
            
            recipesRepository.Update(recipe);
            await unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> Remove(Guid recipeId)
        {
            var recipe = await recipesRepository.FindAsync(recipeId);
            if (recipe == null)
                return false;

            recipesRepository.Remove(recipe);
            await unitOfWork.CommitAsync();
            return true;
        }

        public Task<List<Recipe>> AllAsync()
        {
            return recipesRepository.AllAsync();
        }

        public Task<Recipe> FindAsync(Guid recipeId)
        {
            return recipesRepository.FindAsync(recipeId);
        }

        private async Task<List<RecipeTag>> CreateTagsForRecipe(Recipe recipe, IList<Tag> tags)
        {
            var recipeTags = new List<RecipeTag>();

            foreach (var newTag in tags)
            {
                var tag = await tagsRepository.FindAsync(newTag.Id);
                if (tag == null)
                {
                    tag = newTag;
                    tagsRepository.Add(tag);
                }
                recipeTags.Add(RecipeTag.Create(tag, recipe.Id));
            }

            return recipeTags;
        }
    }
}
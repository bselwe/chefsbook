using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsBook.Core.Models;
using ChefsBook.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository recipesRepository;
        private readonly ITagsService tagsService;
        private readonly CoreUnitOfWork unitOfWork;

        public RecipesService(
            IRecipesRepository recipesRepository, 
            ITagsService tagsService,
            CoreUnitOfWork unitOfWork)
        {
            this.recipesRepository = recipesRepository;
            this.tagsService = tagsService;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(
            Guid userId, string title, string description, string image, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags)
        {
            var recipe = Recipe.Create(userId, title, description, image, duration, servings, notes, ingredients, steps);
            var recipeTags = await CreateRecipeTags(recipe, tags);
            recipe.AddTags(recipeTags);

            recipesRepository.Add(recipe);
            await unitOfWork.CommitAsync();
        }

        public async Task<bool> UpdateAsync(
            Guid recipeId, Guid userId, string title, string description, string image, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps, IList<Tag> tags)
        {
            var recipe = await recipesRepository.FindAsync(recipeId);
            if (recipe == null || recipe.UserId != userId)
                return false;

            var recipeTags = await CreateRecipeTags(recipe, tags);
            recipe.Update(title, description, image, duration, servings, notes, ingredients, steps);
            recipe.UpdateTags(recipeTags);
            
            await unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(Guid recipeId, Guid userId)
        {
            var recipe = await recipesRepository.FindAsync(recipeId);
            if (recipe == null || recipe.UserId != userId)
                return false;

            recipesRepository.Remove(recipe);
            await unitOfWork.CommitAsync();
            return true;
        }

        public Task<List<Recipe>> AllAsync()
        {
            return recipesRepository.AllAsync();
        }

        public Task<List<Recipe>> AllByUserAsync(Guid userId)
        {
            return recipesRepository.AllByUserAsync(userId);
        }

        public Task<List<Recipe>> FilterAsync(string text, IList<string> tags)
        {
           return recipesRepository.FilterAsync(text, tags);
        }

        public Task<Recipe> FindAsync(Guid recipeId)
        {
            return recipesRepository.FindAsync(recipeId);
        }

        private async Task<List<RecipeTag>> CreateRecipeTags(Recipe recipe, IList<Tag> tags)
        {
            var existingTags = recipe.Tags
                .Where(rt => tags.Select(t => t.Name.ToLower()).Contains(rt.Tag.Name.ToLower()));

            var newTags = tags
                .Where(rt => !existingTags.Select(t => t.Tag.Name.ToLower()).Contains(rt.Name.ToLower()));

            var recipeTags = new List<RecipeTag>(existingTags);
            foreach (var tag in newTags)
            {
                var newTag = await tagsService.FindAsync(tag.Name) ?? tag;
                recipeTags.Add(RecipeTag.Create(newTag, recipe.RecipeId));
            }

            return recipeTags;
        }
    }
}
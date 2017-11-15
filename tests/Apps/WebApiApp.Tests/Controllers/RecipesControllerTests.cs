using System;
using System.Collections.Generic;
using ChefsBook.Core;
using ChefsBook.Core.Models;
using ChefsBook.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using System.Collections;
using ChefsBook.WebApiApp.Controllers;
using ChefsBook.Core.Contracts;

namespace ChefsBook.WebApiApp.Tests
{
    public class RecipesControllerTests
    {
        static RecipesControllerTests()
        {
            AutoMapper.Mapper.Initialize(cfg => { cfg.AddProfile<CoreProfile>(); });
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task GetRecipes_Returns_Empty_List_When_Recipes_Dont_Exist()
        {
            // Arrange
            var recipesService = Substitute.For<IRecipesService>();
            recipesService.AllAsync().Returns(new List<Recipe>());
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);

            // Act
            var result = await controller.GetRecipes();

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(OkObjectResult)));
            Assert.Equal(new List<Recipe>(), (result as OkObjectResult).Value);
        }

        [Fact]
        public async Task GetRecipes_Returns_List_When_Recipes_Exist()
        {
            // Arrange
            var testRecipes = GenerateRecipes();
            var recipesService = Substitute.For<IRecipesService>();
            recipesService.AllAsync().Returns(testRecipes);
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);

            // Act
            var result = await controller.GetRecipes();

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(OkObjectResult)));
            Assert.Equal(testRecipes.Count, ((IList) (result as OkObjectResult).Value).Count);
        }

        [Fact]
        public async Task GetRecipeById_Returns_NotFound_When_Recipe_Doesnt_Exist()
        {
            // Arrange
            var recipesService = Substitute.For<IRecipesService>();
            recipesService.FindAsync(Arg.Any<Guid>()).Returns(null as Recipe);
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);

            // Act
            var result = await controller.GetRecipeById(Guid.NewGuid());

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(NotFoundResult)));
        }

        [Fact]
        public async Task FilterRecipes_Returns_BadRequest_When_Arguments_Are_Invalid()
        {
            // Arrange
            var recipesService = Substitute.For<IRecipesService>();
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);
            var filter = new FilterRecipeDTO { Text = null, Tags = null };

            // Act
            var result = await controller.FilterRecipes(filter);

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(BadRequestResult)));
        }

        [Fact]
        public async Task CreateRecipe_Returns_Ok_When_Arguments_Are_Valid()
        {
            // Arrange
            var recipesService = Substitute.For<IRecipesService>();
            recipesService
                .Create(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<TimeSpan?>(), Arg.Any<int?>(), Arg.Any<string>(), Arg.Any<IList<Ingredient>>(), Arg.Any<IList<Step>>(), Arg.Any<IList<Tag>>())
                .Returns(Task.CompletedTask);
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);
            var recipe = new NewRecipeDTO { 
                Title = "Naleśniki", 
                Description = "Naleśniki z serem", 
                Image = null,
                Duration = TimeSpan.FromMinutes(30), 
                Servings = 12,
                Notes = "Idealne na lekki głód",
                Ingredients = new List<NewRecipeIngredientDTO>(),
                Steps = new List<NewRecipeStepDTO>(),
                Tags = new List<NewRecipeTagDTO>()
            };

            // Act
            var result = await controller.CreateRecipe(recipe);

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(OkResult)));
        }

        [Fact]
        public async Task CreateRecipe_Returns_BadRequest_When_Arguments_Are_Invalid()
        {
            // Arrange
            var recipesService = Substitute.For<IRecipesService>();
            recipesService
                .Create(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<TimeSpan?>(), Arg.Any<int?>(), Arg.Any<string>(), Arg.Any<IList<Ingredient>>(), Arg.Any<IList<Step>>(), Arg.Any<IList<Tag>>())
                .Returns(Task.CompletedTask);
            var controller = new RecipesController(recipesService, AutoMapper.Mapper.Instance);
            var recipe = new NewRecipeDTO();

            // Act
            var result = await controller.CreateRecipe(recipe);

            // Assert
            Assert.True(result.GetType().IsAssignableFrom(typeof(BadRequestResult)));
        }

        private List<Recipe> GenerateRecipes()
        {
            return new List<Recipe>()
            {
                Recipe.Create(
                    "Naleśniki", 
                    "Naleśniki z serem", 
                    null,
                    TimeSpan.FromMinutes(30), 
                    12,
                    "Idealne na lekki głód",
                    new List<Ingredient>() { Ingredient.Create("mleko", "1l") },
                    new List<Step>() { Step.Create(TimeSpan.FromMinutes(5), "Zrób ciasto") }
                ),
                Recipe.Create(
                    "Jajecznica", 
                    null,
                    null, 
                    null, 
                    null, 
                    null, 
                    null, 
                    null
                ),
                Recipe.Create(
                    "Spaghetti", 
                    "Spaghetti bolognese", 
                    null,
                    TimeSpan.FromMinutes(45), 
                    2, 
                    null,
                    null, 
                    null
                )
            };
        }
    }
}

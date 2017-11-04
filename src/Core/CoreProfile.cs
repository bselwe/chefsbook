using System;
using AutoMapper;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Models;

namespace ChefsBook.Core
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Step, StepDTO>();
        }
    }
}

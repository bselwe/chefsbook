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
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<StepDTO, Step>();
            CreateMap<TagDTO, Tag>();
        }
    }
}

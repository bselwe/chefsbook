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
            CreateMap<Recipe, RecipeDetailsDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Step, StepDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<RecipeTag, TagDTO>()
                .ForMember(dest => dest.Name, c => c.MapFrom(src => src.Tag.Name));
        }
    }
}

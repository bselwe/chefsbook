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
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.RecipeId));

            CreateMap<Recipe, RecipeDetailsDTO>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.RecipeId));

            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Step, StepDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<RecipeTag, TagDTO>()
                .ForMember(dest => dest.Name, c => c.MapFrom(src => src.Tag.Name));

            CreateMap<CartItem, CartItemDTO>();
        }
    }
}

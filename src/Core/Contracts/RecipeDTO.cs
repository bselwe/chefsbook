using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
    }
}
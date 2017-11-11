using System;
using System.Collections.Generic;

namespace Core.Contracts
{
    public class FilterRecipeDTO
    {
        public string Text { get; set; }
        public List<Guid> Tags { get; set; }
    }
}
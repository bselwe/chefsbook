using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class FilterRecipeDTO
    {
        public string Text { get; set; }
        public List<Guid> Tags { get; set; }
    }
}
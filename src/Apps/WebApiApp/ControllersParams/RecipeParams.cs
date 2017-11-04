using System;

namespace WebApiApp.ControllersParams
{
    public class RecipeParams
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
    }
}

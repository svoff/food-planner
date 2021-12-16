using FoodPlanner.Common;
using System.Configuration;

namespace FoodPlanner.Data
{
    public class RecipeDatabase : IRecipeDataProvider
    {
        public RecipeDatabase() 
        {
            RecipesFolder = ConfigurationManager.AppSettings["RecipesFolder"] ?? string.Empty;
        }

        private string RecipesFolder { get; set; }

        private List<Recipe> AllRecipes { get; set; } = new();

        public IEnumerable<Recipe> FindRecipes(string searchString)
        {
            if (AllRecipes.Count == 0)
            {                
                if (!string.IsNullOrEmpty(RecipesFolder))
                {
                    AllRecipes = Directory.GetFiles(RecipesFolder, "*.yaml").Select(f => RecipeIO.ReadRecipeFile(f)).ToList();
                }
            }

            return AllRecipes.Where(r => IsMatch(r, searchString));
        }

        private bool IsMatch(Recipe arg, string searchString)
        {
            return true;
        }

        public string? GetRecipeUrl(Recipe recipe)
        {
            // Note: leave dealing with missing descriptions to the UI
            // TODO: add cache if sluggish

            if (!string.IsNullOrEmpty(RecipesFolder) && recipe.DescriptionFile is string file)
            {
                string path = Path.Combine(RecipesFolder, file);
                return File.Exists(path) ? path : null;
            }

            return null;
        }
    }
}
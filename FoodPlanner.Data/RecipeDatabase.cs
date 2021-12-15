using FoodPlanner.Common;
using System.Configuration;

namespace FoodPlanner.Data
{
    public class RecipeDatabase : IRecipeDataProvider
    {
        public RecipeDatabase()
        {

        }

        public IEnumerable<Recipe> FindRecipes(string searchString)
        {
            var folder = ConfigurationManager.AppSettings["RecipesFolder"];
            return string.IsNullOrEmpty(folder) ? 
            {
                Directory.GetFiles(folder, "*.yaml").Select(f => RecipeIO.ReadRecipeFile(f)).Where(r => IsMatch(r, searchString))
            }
            Console.WriteLine(folder);
            return Enumerable.Empty<Recipe>();
        }

        private bool IsMatch(Recipe arg, string searchString)
        {
            return true;
        }

        public string? GetDescriptionText(string descriptionFile)
        {
            throw new NotImplementedException();
        }
    }
}
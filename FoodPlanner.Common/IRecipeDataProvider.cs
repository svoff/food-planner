namespace FoodPlanner.Common
{
    public interface IRecipeDataProvider
    {
        IEnumerable<Recipe> FindRecipes(string searchString);

        string? GetDescriptionText(string descriptionFile);
    }
}

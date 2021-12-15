namespace FoodPlanner.Common
{
    public record class Recipe
    {
        public Recipe(string name, string descriptionFile, int servings, string[] ingredients, string[]? tags)
        {
            Name = name;
            DescriptionFile = descriptionFile;
            Servings = servings;
            Ingredients = ingredients;
            Tags = tags ?? Array.Empty<string>();
        }

        public string Name { get; init; }

        public string DescriptionFile { get; init; }

        public int Servings { get; init; }

        public string[] Ingredients { get; init; }

        public string[] Tags { get; init; }
    }
}   
namespace FoodPlanner.Common
{
    public record class Recipe
    {
        public Recipe(string name, string description, Ingredient[] ingredients, string[]? tags)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Tags = tags ?? Array.Empty<string>();
        }

        /// <summary>
        /// The name is used as an informal id for user interaction.  Preferably unique.
        /// It may consist of several words but should be short enough for use as a single-line header.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Describes the steps required to make the dish.  The text is on Markdown format and may (should) include images.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// The list of ingredients for the recipe includes everything that's needed to make the dish.  The ingredients for
        /// several recipes can be combined into a shopping list.
        /// </summary>
        public Ingredient[] Ingredients { get; init; }

        public string[] Tags { get; init; }
    }
}   
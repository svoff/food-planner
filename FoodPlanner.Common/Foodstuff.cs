namespace FoodPlanner.Common
{
    public record class Foodstuff
    {
        public Foodstuff(string name, string[] tags)
        {
            Name = name;
            Tags = tags;
        }

        public string Name { get; init; }    

        public string[] Tags { get; init; }
    }
}
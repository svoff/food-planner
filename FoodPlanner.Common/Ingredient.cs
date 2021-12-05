namespace FoodPlanner.Common
{
    public record class Ingredient
    {
        public Ingredient(Foodstuff foodstuff, int quantity, string unit)
        {
            Foodstuff = foodstuff;
            Quantity = quantity;
            Unit = unit;
        }

        public Foodstuff Foodstuff { get; init; }

        public int Quantity { get; init;}

        public string Unit { get; init; }
    }
}
using FoodPlanner.Common;

namespace FoodPlanner.Data
{
    public class MockDatabase : IFoodDatabase
    {
        public MockDatabase()
        {

        }

        public IEnumerable<Foodstuff> FindFoodstuff(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> FindRecipes(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlanner.Common
{
    public interface IFoodDatabase
    {
        IEnumerable<Recipe> FindRecipes(string searchString);

        IEnumerable<Foodstuff> FindFoodstuff(string searchString);
    }
}

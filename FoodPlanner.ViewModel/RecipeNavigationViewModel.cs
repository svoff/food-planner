using FoodPlanner.Common;
using System.Collections.ObjectModel;

namespace FoodPlanner.ViewModel
{
    public class RecipeNavigationViewModel : ViewModelBase
    {
        private readonly IRecipeDataProvider _recipeDataProvider;
        private Recipe _selectedRecipe;
        private string _searchFilter = string.Empty;

        public RecipeNavigationViewModel(IRecipeDataProvider recipeDataProvider)
        {
            _recipeDataProvider = recipeDataProvider;
        }

        public ObservableCollection<Recipe> Recipes { get; } = new();

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }

            set
            {
                if (_selectedRecipe != value)
                {
                    _selectedRecipe = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsRecipeSelected));
                }
            }
        }

        public bool IsRecipeSelected => SelectedRecipe != null;

        public string SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                if (_searchFilter != value)
                {
                    _searchFilter = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string GetDescriptionTextForRecipe(Recipe recipe)
        {
            return _recipeDataProvider.GetDescriptionText(recipe.DescriptionFile);
        }

        public void Load()
        {
            var recipes = _recipeDataProvider.FindRecipes(SearchFilter);

            Recipes.Clear();
            foreach (var recipe in recipes)
            {
                Recipes.Add(recipe);    
            }
        }
    }
}
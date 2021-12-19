using FoodPlanner.Common;
using System.Collections.ObjectModel;

namespace FoodPlanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRecipeDataProvider _recipeDataProvider;
        private Recipe? _selectedRecipe;
        private string _searchFilter;

        public MainViewModel(IRecipeDataProvider recipeDataProvider)
        {
            _recipeDataProvider = recipeDataProvider;
            _selectedRecipe = null;
            _searchFilter = String.Empty;
        }

        public ObservableCollection<Recipe> Recipes { get; } = new();

        public ObservableCollection<string> Ingredients { get; } = new();

        public Recipe? SelectedRecipe
        {
            get { return _selectedRecipe; }

            set
            {
                if (_selectedRecipe != value)
                {
                    _selectedRecipe = value;
                    UpdateIngredients();
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsRecipeSelected));
                    RaisePropertyChanged(nameof(SelectedRecipeUrl));
                    RaisePropertyChanged(nameof(Ingredients));
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

        public string SelectedRecipeUrl
        {
            get
            {
                if (_selectedRecipe is Recipe r && _recipeDataProvider.GetRecipeUrl(r) is string url)
                    return url;
                else
                    return "http://www.microsoft.com";
            }
        }

        private void UpdateIngredients()
        {
            Ingredients.Clear();
            if (_selectedRecipe != null)
            {
                foreach (var s in _selectedRecipe.Ingredients)
                {
                    Ingredients.Add(s);
                }
            }
        }

        public void LoadRecipes()
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
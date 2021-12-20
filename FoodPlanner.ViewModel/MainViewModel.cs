using FoodPlanner.Common;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace FoodPlanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRecipeDataProvider _recipeDataProvider;
        private Recipe? _selectedRecipe;

        public MainViewModel(IRecipeDataProvider recipeDataProvider)
        {
            _recipeDataProvider = recipeDataProvider;
            _selectedRecipe = null;
        }

        public List<Recipe> Recipes { get; } = new();

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

        public bool TrySelectRecipe(string? recipeName)
        {
            if (recipeName is not null && Recipes.FirstOrDefault(r => r.Name == recipeName) is Recipe r)
            {
                SelectedRecipe = r;
                return true;
            }
            else
            {
                SelectedRecipe = null;
                return false;
            }
        }

        public void LoadRecipes()
        {
            var recipes = _recipeDataProvider.FindRecipes(string.Empty);

            Recipes.Clear();
            foreach (var recipe in recipes)
            {
                Recipes.Add(recipe);
            }
        }
    }
}
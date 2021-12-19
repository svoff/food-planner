using FoodPlanner.Common;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace FoodPlanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRecipeDataProvider _recipeDataProvider;
        private Recipe? _selectedRecipe;
        private WeekPlanItem? _selectedWeekPlanItem;

        // Null means don't search, empty string means match all recipes.
        private string? _searchString;

        public MainViewModel(IRecipeDataProvider recipeDataProvider, string[] weekDays)
        {
            _recipeDataProvider = recipeDataProvider;
            _selectedRecipe = null;
            _selectedWeekPlanItem = null;
            _searchString = null;

            WeekPlanItems.Clear();
            for (int i = 0; i < 7; ++i)
            {
                WeekPlanItems.Add(new WeekPlanItem(weekDays[i], "boobo", null));
            }
        }

        public record WeekPlanItem(string WeekDay, string SearchString, Recipe? SelectedRecipe);

        public ObservableCollection<WeekPlanItem> WeekPlanItems { get; } = new();

        public ObservableCollection<Recipe> Recipes { get; } = new();

        public ObservableCollection<string> Ingredients { get; } = new();

        public WeekPlanItem? SelectedWeekPlanItem
        {
            get { return _selectedWeekPlanItem; }

            set
            {
                if (_selectedWeekPlanItem != value)
                {
                    _selectedWeekPlanItem = value;
                    RaisePropertyChanged();

                    if (value is not null)
                    {
                        SearchString = value.SearchString;
                        SelectedRecipe = value.SelectedRecipe;
                    }
                    else
                    {
                        SearchString = null;
                        SelectedRecipe = null;
                    }

                }
            }
        }

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

        public string? SearchString
        {
            get { return _searchString; }

            set
            {
                if (_searchString != value)
                {
                    _searchString = value;
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
            var recipes = _recipeDataProvider.FindRecipes(SearchString);

            Recipes.Clear();
            foreach (var recipe in recipes)
            {
                Recipes.Add(recipe);    
            }
        }
    }
}
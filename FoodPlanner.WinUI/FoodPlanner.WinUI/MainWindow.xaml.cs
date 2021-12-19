using FoodPlanner.Common;
using FoodPlanner.Data;
using FoodPlanner.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FoodPlanner.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel(new RecipeDatabase());
            this.Activated += MainWindow_Activated;
        }

        public MainViewModel ViewModel { get; }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (ViewModel.Recipes.Count == 0)
            {
                ViewModel.LoadRecipes();
            }
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Weekday_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = ViewModel.Recipes;
            }
        }

        private void Weekday_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion is Recipe r)
            {
                ViewModel.SelectedRecipe = r;
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }

        private void Weekday_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Recipe r)
            {
                sender.Text = r.Name;
                ViewModel.SelectedRecipe = r;
            }
        }
    }
}

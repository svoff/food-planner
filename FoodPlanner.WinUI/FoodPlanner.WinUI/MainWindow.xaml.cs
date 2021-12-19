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
            var firstDay = ConfigurationManager.AppSettings["FirstDayOfWeekPlan"] ?? "Saturday";

            var weekDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var offset = weekDays.IndexOf(firstDay);
            Debug.Assert(offset >= 0);

            var localizedWeekdays = new List<string>();
            for (int i = 0; i < 7; ++i)
            {
                var day = weekDays[(i + offset) % 7];
                var localizedDay = Application.Current.Resources[day] as string;
                Debug.Assert(localizedDay != null);
                localizedWeekdays.Add(localizedDay);
            }
            ViewModel = new MainViewModel(new RecipeDatabase(), localizedWeekdays.ToArray());
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
    }
}

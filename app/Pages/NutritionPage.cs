using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;
using cursiv.Services;
using System.Collections.Generic;

namespace cursiv.Pages
{
    public class NutritionPage : ContentPage
    {
        private AIService aiService;
        private Entry availableProductsEntry;
        private Label resultLabel;
        private ListView mealsListView;
        private Button generateMealButton;
        private double protein, fat, carbs;

        public NutritionPage()
        {
            Title = "Харчування";
            aiService = new AIService();

            availableProductsEntry = new Entry { Placeholder = "Введіть наявні продукти (через кому)" };
            generateMealButton = new Button { Text = "Отримати раціон" };
            generateMealButton.Clicked += async (s, e) => await GetMealPlan();

            resultLabel = new Label { Text = "Натисніть кнопку для отримання раціону..." };
            mealsListView = new ListView { IsVisible = false };

            Content = new StackLayout
            {
                Padding = 20,
                Children = { availableProductsEntry, generateMealButton, resultLabel, mealsListView }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadPreviousCalculations();
        }

        private void LoadPreviousCalculations()
        {
            protein = Preferences.Get("Protein", 0.0);
            fat = Preferences.Get("Fat", 0.0);
            carbs = Preferences.Get("Carbs", 0.0);
        }

        private async Task GetMealPlan()
        {
            generateMealButton.IsEnabled = false;
            resultLabel.Text = "Генерація раціону...";

            string availableProducts = availableProductsEntry.Text ?? "Немає вказаних продуктів";
            var meals = await aiService.GetMealPlanAsync(protein, fat, carbs, availableProducts);

            if (meals.Count > 0)
            {
                mealsListView.ItemsSource = meals;
                mealsListView.IsVisible = true;
                resultLabel.Text = "Ось ваш рекомендований раціон:";
            }
            else
            {
                resultLabel.Text = "❌ Не вдалося отримати раціон. Спробуйте ще раз.";
            }

            generateMealButton.IsEnabled = true;
        }
    }
}

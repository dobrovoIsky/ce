using Microsoft.Maui.Controls;
using cursiv.Pages;

namespace cursiv
{
    public class MenuPage : ContentPage
    {
        public MenuPage()
        {
            Title = "Меню";
            ListView menuList = new ListView
            {
                ItemsSource = new string[]
                        {
                            "Головна",
                            "Розрахунок БЖУ",
                            "Вхід",
                            "Реєстрація",
                            "Історія харчування"
                        }

            };
            menuList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                Page page = e.SelectedItem.ToString() switch
                {
                    "Головна" => new HomePage(),
                    "Розрахунок БЖУ" => new CalculationPage(),
                    "Вхід" => new LoginPage(),
                    "Реєстрація" => new RegisterPage(),
                    "Харчування" => new NutritionPage(),
                    "Історія харчування" => new cursiv.Pages.MealHistoryPage(),

                    _ => new HomePage()
                };
                if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyoutPage)
                {
                    flyoutPage.Detail = new NavigationPage(page);
                    flyoutPage.IsPresented = false;
                }
            };
            Content = new StackLayout { Children = { menuList } };
        }
    }
}


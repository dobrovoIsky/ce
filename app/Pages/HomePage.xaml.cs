using Microsoft.Maui.Controls;

namespace cursiv
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Title = "Головна";
            Button calcButton = new Button { Text = "Перейти до розрахунку БЖУ" };
            calcButton.Clicked += (s, e) =>
            {
                if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyoutPage)
                {
                    flyoutPage.Detail = new NavigationPage(new CalculationPage());
                }
            };
            Content = new StackLayout
            {
                Padding = 20,
                Children = { new Label { Text = "Ласкаво просимо!" }, calcButton }
            };
        }
    }
}

using Microsoft.Maui.Controls;

namespace cursiv
{
    public class MainPage : FlyoutPage
    {
        public MainPage()
        {
            Flyout = new MenuPage();
            Detail = new NavigationPage(new HomePage());
        }
    }
}
    
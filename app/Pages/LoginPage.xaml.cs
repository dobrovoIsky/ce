using Microsoft.Maui.Controls;

namespace cursiv
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Title = "Вхід";
            Entry usernameEntry = new Entry { Placeholder = "Логін" };
            Entry passwordEntry = new Entry { Placeholder = "Пароль", IsPassword = true };
            Button loginButton = new Button { Text = "Увійти" };

            Content = new StackLayout
            {
                Padding = 20,
                Children = { usernameEntry, passwordEntry, loginButton }
            };
        }
    }
}

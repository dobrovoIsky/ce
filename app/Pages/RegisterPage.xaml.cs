using Microsoft.Maui.Controls;
using cursiv.Services;

namespace cursiv
{
    public class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            Title = "Реєстрація";
            Entry usernameEntry = new Entry { Placeholder = "Логін" };
            Entry passwordEntry = new Entry { Placeholder = "Пароль", IsPassword = true };
            Entry weightEntry = new Entry { Placeholder = "Вага (кг)", Keyboard = Keyboard.Numeric };
            Entry heightEntry = new Entry { Placeholder = "Зріст (см)", Keyboard = Keyboard.Numeric };
            Entry ageEntry = new Entry { Placeholder = "Вік", Keyboard = Keyboard.Numeric };
            Picker goalPicker = new Picker { Title = "Мета", ItemsSource = new List<string> { "схуднення", "підтримка", "набір" } };

            Button registerButton = new Button { Text = "Зареєструватися" };
            Label resultLabel = new Label();

            registerButton.Clicked += async (s, e) =>
            {
                var dbService = new DatabaseService();
                bool success = await dbService.RegisterUser(usernameEntry.Text, passwordEntry.Text,
                                                            double.Parse(weightEntry.Text), double.Parse(heightEntry.Text),
                                                            int.Parse(ageEntry.Text), goalPicker.SelectedItem.ToString());

                resultLabel.Text = success ? "✅ Реєстрація успішна!" : "❌ Помилка реєстрації.";
            };

            Content = new StackLayout
            {
                Padding = 20,
                Children = { usernameEntry, passwordEntry, weightEntry, heightEntry, ageEntry, goalPicker, registerButton, resultLabel }
            };
        }
    }
}



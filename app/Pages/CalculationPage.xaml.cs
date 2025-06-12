using Microsoft.Maui.Controls;

namespace cursiv
{
    public class CalculationPage : ContentPage
    {
        public CalculationPage()
        {
            Title = "Розрахунок БЖУ";
            Entry heightEntry = new Entry { Placeholder = "Зріст (см)", Keyboard = Keyboard.Numeric };
            Entry weightEntry = new Entry { Placeholder = "Вага (кг)", Keyboard = Keyboard.Numeric };
            Entry ageEntry = new Entry { Placeholder = "Вік", Keyboard = Keyboard.Numeric };
            Picker activityPicker = new Picker
            {
                Title = "Рівень активності",
                ItemsSource = new string[] { "Малорухливий", "Помірний", "Активний" }
            };
            Button calculateButton = new Button { Text = "Розрахувати" };
            Label resultLabel = new Label { Text = "Результат з'явиться тут" };

            calculateButton.Clicked += (s, e) =>
            {
                if (double.TryParse(heightEntry.Text, out double height) &&
                    double.TryParse(weightEntry.Text, out double weight) &&
                    int.TryParse(ageEntry.Text, out int age) &&
                    activityPicker.SelectedIndex >= 0)
                {
                    double bmr = 10 * weight + 6.25 * height - 5 * age + 5;
                    double factor = activityPicker.SelectedIndex switch
                    {
                        0 => 1.2,
                        1 => 1.55,
                        2 => 1.9,
                        _ => 1.2
                    };
                    double calories = bmr * factor;
                    double protein = (calories * 0.3) / 4;
                    double fat = (calories * 0.25) / 9;
                    double carbs = (calories * 0.45) / 4;

                    resultLabel.Text = $"Калорії: {calories:F1}\nБілки: {protein:F1}г\nЖири: {fat:F1}г\nВуглеводи: {carbs:F1}г";
                }
                else
                {
                    resultLabel.Text = "Будь ласка, введіть коректні дані";
                }
            };

            Content = new StackLayout
            {
                Padding = 20,
                Children = { heightEntry, weightEntry, ageEntry, activityPicker, calculateButton, resultLabel }
            };
        }
    }
}

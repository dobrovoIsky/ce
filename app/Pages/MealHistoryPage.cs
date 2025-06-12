using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace cursiv.Pages
{
    public class MealPlan
    {
        public string Plan { get; set; }
        public DateTime Date { get; set; }
    }

    public class MealHistoryPage : ContentPage
    {
        private readonly ListView _listView;
        private readonly Label _statusLabel;

        public MealHistoryPage()
        {
            Title = "Історія харчування";

            _statusLabel = new Label { Text = "Завантаження..." };

            _listView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var planLabel = new Label { FontSize = 14 };
                    planLabel.SetBinding(Label.TextProperty, "Plan");

                    var dateLabel = new Label { FontSize = 12, TextColor = Colors.Gray };
                    dateLabel.SetBinding(Label.TextProperty, new Binding("Date", stringFormat: "Дата: {0:dd.MM.yyyy}"));

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = 10,
                            Children = { dateLabel, planLabel }
                        }
                    };
                })
            };

            Content = new StackLayout
            {
                Children = { _statusLabel, _listView }
            };

            LoadHistoryAsync();
        }

        private async void LoadHistoryAsync()
        {
            try
            {
                int userId = App.CurrentUserId; // цей userId встановлюється після логіну
                var httpClient = new HttpClient();

                // ⚠️ Замінити localhost на IP якщо телефон
                var response = await httpClient.GetFromJsonAsync<List<MealPlan>>($"\"http://192.168.0.107:7157/api/nutrition/history/{{userId}}\"");

                if (response != null && response.Count > 0)
                {
                    _statusLabel.IsVisible = false;
                    _listView.ItemsSource = response;
                }
                else
                {
                    _statusLabel.Text = "Історія відсутня.";
                }
            }
            catch (Exception ex)
            {
                _statusLabel.Text = $"Помилка завантаження: {ex.Message}";
            }
        }
    }
}

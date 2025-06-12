using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace BjuApiServer.Services
{
    public class OllamaService
    {
        private readonly HttpClient _http;

        public OllamaService(HttpClient http)
        {
            _http = http;
            // Збільшити таймаут для HttpClient
            _http.Timeout = TimeSpan.FromSeconds(200);
        }

        public async Task<string> GetMealPlanAsync(string prompt)
        {
            var request = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            try
            {
                // Відправка запиту до API
                var response = await _http.PostAsync("http://localhost:11434/api/generate", content);

                // Перевірка на успіх
                if (!response.IsSuccessStatusCode)
                {
                    // Логування помилки HTTP запиту
                    Console.WriteLine($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }

                // Читання відповіді
                var result = await response.Content.ReadAsStringAsync();

                // Парсинг JSON
                var json = JsonDocument.Parse(result);

                // Перевірка на наявність властивості "response" в JSON
                if (json.RootElement.TryGetProperty("response", out var mealPlanProperty))
                {
                    return mealPlanProperty.GetString();
                }
                else
                {
                    Console.WriteLine("Error: 'response' property not found in the API response.");
                    return "Error: 'response' property not found in the API response.";
                }
            }
            catch (HttpRequestException ex)
            {
                // Логування помилки при HTTP запиті
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                return $"HttpRequestException: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Логування інших помилок
                Console.WriteLine($"Exception: {ex.Message}");
                return $"Exception: {ex.Message}";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace cursiv.Services
{
    public class AIService
    {
        private const string API_URL = "http://127.0.0.1:11434/api/generate";

        public async Task<List<string>> GetMealPlanAsync(double protein, double fat, double carbs, string availableProducts)
        {
            using var client = new HttpClient();

            var requestBody = new
            {
                model = "mistral", // Або "llama2", "gemma" залежно від встановленої моделі
                prompt = $"Підбери 3 варіанти меню на день з {protein} г білка, {fat} г жиру, {carbs} г вуглеводів. Продукти: {availableProducts}.",
                stream = false
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(API_URL, content);
            if (!response.IsSuccessStatusCode) return new List<string> { "❌ Помилка отримання раціону." };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<OllamaResponse>(jsonResponse);

            return responseData?.Response?.Split('\n').ToList() ?? new List<string> { "❌ Немає відповіді від моделі." };
        }
    }

    public class OllamaResponse
    {
        public string? Response { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using BjuApiServer.Models;
using BjuApiServer.Services;
using BjuApiServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BjuApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly OllamaService _ollama;

        public NutritionController(AppDbContext db, OllamaService ollama)
        {
            _db = db;
            _ollama = ollama;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] User user)
        {
            double bmr = 10 * user.Weight + 6.25 * user.Height - 5 * user.Age + 5;

            double activityFactor = user.ActivityLevel.ToLower() switch
            {
                "малорухливий" => 1.2,
                "помірний" => 1.55,
                "активний" => 1.9,
                _ => 1.55
            };

            double calories = bmr * activityFactor;

            calories *= user.Goal.ToLower() switch
            {
                "схуднення" => 0.8,
                "набір" => 1.2,
                _ => 1.0
            };

            double protein = (calories * 0.3) / 4;
            double fat = (calories * 0.25) / 9;
            double carbs = (calories * 0.45) / 4;

            string prompt = $"Склади приклад денного раціону харчування (сніданок, обід, вечеря, перекуси) для людини з добовим споживанням {calories:F0} ккал, {protein:F0}г білка, {fat:F0}г жиру, {carbs:F0}г вуглеводів.";

            var mealPlan = await _ollama.GetMealPlanAsync(prompt);

            // Зберегти результат
            if (user.Id != 0)
            {
                _db.MealPlans.Add(new MealPlan
                {
                    UserId = user.Id,
                    Plan = mealPlan,
                    Date = DateTime.UtcNow
                });

                await _db.SaveChangesAsync();
            }

            return Ok(new
            {
                Calories = Math.Round(calories),
                Protein = Math.Round(protein),
                Fat = Math.Round(fat),
                Carbs = Math.Round(carbs),
                MealPlan = mealPlan
            });
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetHistory(int userId)
        {
            var history = await _db.MealPlans
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Date)
                .Take(10)
                .ToListAsync();

            return Ok(history);
        }
    }
}

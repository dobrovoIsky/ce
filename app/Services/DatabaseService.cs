using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace cursiv.Services
{
    public class DatabaseService
    {
        private const string ConnectionString = "Server=localhost;Database=bju_calculator;User ID=root;Password=;SslMode=None;";

        public async Task<bool> TestConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Підключення до MySQL успішне!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка підключення: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RegisterUser(string username, string password, double weight, double height, int age, string goal)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO users (username, password, weight, height, age, goal) VALUES (@username, @password, @weight, @height, @age, @goal)";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@weight", weight);
                        cmd.Parameters.AddWithValue("@height", height);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@goal", goal);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                Console.WriteLine("✅ Користувач зареєстрований!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка реєстрації: {ex.Message}");
                return false;
            }
        }

    }
}

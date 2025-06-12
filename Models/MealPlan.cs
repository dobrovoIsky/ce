namespace BjuApiServer.Models;

public class MealPlan
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Plan { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
}

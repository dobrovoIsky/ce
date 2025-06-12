﻿namespace BjuApiServer.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public int Age { get; set; }
    public string Goal { get; set; } = string.Empty;
    public string ActivityLevel { get; set; } = "Normal";
}

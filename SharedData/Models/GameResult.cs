namespace SharedData.Models;

public class GameResult
{
    public int Id { get; set; }
    public int UseerId { get; set; }
    public string Username { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
    
}
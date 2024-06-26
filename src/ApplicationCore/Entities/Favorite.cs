namespace ApplicationCore.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public User User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}
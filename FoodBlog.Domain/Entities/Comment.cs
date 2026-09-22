namespace FoodBlog.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 sao

    // Foreign keys
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
}
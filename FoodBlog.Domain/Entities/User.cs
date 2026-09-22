using FoodBlog.Domain.Enums;

namespace FoodBlog.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
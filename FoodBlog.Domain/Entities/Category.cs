namespace FoodBlog.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
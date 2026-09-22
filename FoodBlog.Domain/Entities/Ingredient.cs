namespace FoodBlog.Domain.Entities;

public class Ingredient : BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public decimal Quantity { get; set; }
	public string Unit { get; set; } = string.Empty;
	public string? Notes { get; set; }

	// Foreign key
	public Guid RecipeId { get; set; }

	// Navigation property
	public Recipe Recipe { get; set; } = null!;
}
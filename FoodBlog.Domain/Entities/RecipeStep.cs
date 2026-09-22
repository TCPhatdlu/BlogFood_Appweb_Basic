namespace FoodBlog.Domain.Entities;

public class RecipeStep : BaseEntity
{
	public int StepNumber { get; set; }
	public string Description { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }

	// Foreign key
	public Guid RecipeId { get; set; }

	// Navigation property
	public Recipe Recipe { get; set; } = null!;
}
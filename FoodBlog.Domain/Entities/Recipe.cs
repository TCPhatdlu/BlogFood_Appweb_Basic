using FoodBlog.Domain.Enums;

namespace FoodBlog.Domain.Entities;

public class Recipe : BaseEntity
{
	public string Title { get; set; } = string.Empty;
	public string? Description { get; set; }
	public string? ImageUrl { get; set; }
	public int PrepTimeMinutes { get; set; }
	public int CookTimeMinutes { get; set; }
	public int Servings { get; set; }
	public DifficultyLevel Difficulty { get; set; }
	public RecipeStatus Status { get; set; } = RecipeStatus.Draft;

	// Foreign keys
	public Guid CategoryId { get; set; }
	public Guid UserId { get; set; }

	// Navigation properties
	public Category Category { get; set; } = null!;
	public User User { get; set; } = null!;
	public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
	public ICollection<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
	public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
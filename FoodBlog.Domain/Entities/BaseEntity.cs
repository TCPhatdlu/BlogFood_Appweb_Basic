namespace FoodBlog.Domain.Entities;

public abstract class BaseEntity
{
	public Guid Id { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
	public string? CreatedBy { get; set; }
}
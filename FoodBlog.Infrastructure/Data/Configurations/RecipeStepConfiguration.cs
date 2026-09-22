using FoodBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBlog.Infrastructure.Data.Configurations;

public class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(s => s.ImageUrl)
            .HasMaxLength(500);

        builder.HasOne(s => s.Recipe)
            .WithMany(r => r.Steps)
            .HasForeignKey(s => s.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => new { s.RecipeId, s.StepNumber })
            .IsUnique();
    }
}
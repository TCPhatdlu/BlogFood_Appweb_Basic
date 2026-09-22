using FoodBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBlog.Infrastructure.Data.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Quantity)
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.Unit)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(200);

        builder.HasOne(i => i.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
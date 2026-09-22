using Bogus;
using FoodBlog.Domain.Entities;
using FoodBlog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodBlog.Infrastructure.Data.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        Console.WriteLine("🔍 Checking if database needs seeding...");

        // Kiểm tra nếu đã có dữ liệu thì không seed lại
        if (await context.Categories.AnyAsync())
        {
            Console.WriteLine("⚠️ Database already has Categories. Skipping seed to prevent duplicates.");
            return;
        }

        Console.WriteLine("🚀 Starting to seed database...");
        var faker = new Faker("vi");

        // 1. Seed Users (5 người dùng)
        Console.WriteLine("  ➜ Seeding 5 Users...");
        var users = new Faker<User>()
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, _ => "AQAAAAIAAYagAAAAEH...")
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
            .RuleFor(u => u.IsActive, _ => true)
            .Generate(5);
        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        // 2. Seed Categories (20 danh mục)
        Console.WriteLine("  ➜ Seeding 20 Categories...");
        var categoryNames = new List<string>
        {
            "Món khai vị", "Món chính", "Món chay", "Tráng miệng", "Đồ uống",
            "Món lẩu", "Món nướng", "Món xào", "Món canh", "Món salad",
            "Bánh ngọt", "Bánh mì", "Món hải sản", "Món gà", "Món bò",
            "Món heo", "Món ăn sáng", "Món ăn vặt", "Món đặc sản", "Món ăn kiêng"
        };

        var categories = categoryNames.Select(name => new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = faker.Lorem.Sentence(),
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        }).ToList();

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // 3. Seed Recipes, Ingredients, và RecipeSteps
        Console.WriteLine("  ➜ Seeding 100 Recipes, 1000 Ingredients, and 500 Steps...");
        var recipes = new List<Recipe>();
        var allIngredients = new List<Ingredient>();
        var allSteps = new List<RecipeStep>();

        var recipeFaker = new Faker<Recipe>("vi")
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.Title, f => f.Lorem.Sentence(3))
            .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
            .RuleFor(r => r.PrepTimeMinutes, f => f.Random.Number(10, 60))
            .RuleFor(r => r.CookTimeMinutes, f => f.Random.Number(15, 120))
            .RuleFor(r => r.Servings, f => f.Random.Number(2, 8))
            .RuleFor(r => r.Difficulty, f => f.PickRandom<DifficultyLevel>())
            .RuleFor(r => r.Status, _ => RecipeStatus.Published)
            .RuleFor(r => r.CategoryId, f => f.PickRandom(categories).Id)
            .RuleFor(r => r.UserId, f => f.PickRandom(users).Id);

        var generatedRecipes = recipeFaker.Generate(100);
        recipes.AddRange(generatedRecipes);

        foreach (var recipe in generatedRecipes)
        {
            var recipeIngredients = new Faker<Ingredient>("vi")
                .RuleFor(i => i.Id, _ => Guid.NewGuid())
                .RuleFor(i => i.Name, f => f.Commerce.ProductName())
                .RuleFor(i => i.Quantity, f => f.Random.Decimal(10, 500))
                .RuleFor(i => i.Unit, f => f.PickRandom(new[] { "gam", "ml", "muỗng canh", "củ", "quả", "nhánh", "tép" }))
                .RuleFor(i => i.RecipeId, recipe.Id)
                .Generate(10);
            allIngredients.AddRange(recipeIngredients);

            var recipeSteps = new Faker<RecipeStep>("vi")
                .RuleFor(s => s.Id, _ => Guid.NewGuid())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.RecipeId, recipe.Id)
                .Generate(5);

            for (int i = 0; i < 5; i++)
            {
                recipeSteps[i].StepNumber = i + 1;
            }
            allSteps.AddRange(recipeSteps);
        }

        await context.Recipes.AddRangeAsync(recipes);
        await context.Ingredients.AddRangeAsync(allIngredients);
        await context.RecipeSteps.AddRangeAsync(allSteps);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ DATABASE SEEDING COMPLETED SUCCESSFULLY!");
    }
}
using FoodBlog.API.Endpoints; // <-- Thêm dòng này
using FoodBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodBlog.API.Endpoints;

public static class DataVerificationEndpoints
{
    public static void MapDataVerificationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/data-verification", async (AppDbContext context) =>
        {
            // 1. Đếm số lượng Categories
            var categoriesCount = await context.Categories.CountAsync();

            // 2. Đếm số lượng Recipes
            var recipesCount = await context.Recipes.CountAsync();

            // 3. Đếm số lượng Recipe có ĐÚNG hoặc LỚN HƠN 10 Ingredients
            var recipesWithEnoughIngredients = await context.Recipes
                .CountAsync(r => r.Ingredients.Count >= 10);

            // 4. Đếm số lượng Recipe có ĐÚNG hoặc LỚN HƠN 5 Steps
            var recipesWithEnoughSteps = await context.Recipes
                .CountAsync(r => r.Steps.Count >= 5);

            // 5. Đánh giá tính toàn vẹn
            var isValid = categoriesCount >= 20
                       && recipesCount >= 100
                       && recipesWithEnoughIngredients == recipesCount
                       && recipesWithEnoughSteps == recipesCount;

            return Results.Ok(new
            {
                IsValid = isValid,
                Summary = new
                {
                    CategoriesCount = categoriesCount,
                    RecipesCount = recipesCount,
                    RecipesWithAtLeast10Ingredients = recipesWithEnoughIngredients,
                    RecipesWithAtLeast5Steps = recipesWithEnoughSteps
                },
                Message = isValid
                    ? "✅ Dữ liệu đã được seed thành công và đảm bảo tính toàn vẹn!"
                    : "❌ Dữ liệu chưa đạt yêu cầu. Vui lòng kiểm tra lại DatabaseSeeder."
            });
        })
        .WithName("GetDataVerification")
        .WithTags("Data Verification")
        .WithOpenApi(); // Hỗ trợ hiển thị đẹp trên Swagger
    }
}
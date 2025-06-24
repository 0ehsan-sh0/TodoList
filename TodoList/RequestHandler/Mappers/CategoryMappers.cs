using TodoList.Database.Models;
using TodoList.RequestHandler.Requests.Category;
using TodoList.RequestHandler.Responces.Category;

namespace TodoList.RequestHandler.Mappers
{
    public static class CategoryMappers
    {
        public static RCategory ToRCategory(this Category category)
        {
            return new RCategory
            {
                id = category.Id,
                name = category.Name,
                description = category.Description,
                color = category.Color,
                created_at = category.Created_at,
            };
        }

        public static Category ToCategory(this CreateCategoryRequest category)
        {
            return new Category
            {
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
            };
        }

        public static Category ToCategory(this UpdateCategoryRequest category, int id)
        {
            return new Category
            {
                Id = id,
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
            };
        }

        public static Category ToCategory(this UpdateCategoryRequest category, int id, string username)
        {
            return new Category
            {
                Id = id,
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
                Username = username
            };
        }
    }
}

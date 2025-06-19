using TodoList.Database.Models;
using TodoList.RequestHandler.Requests;
using TodoList.RequestHandler.Responces.Category;

namespace TodoList.RequestHandler.Mappers
{
    public static class CategoryMappers
    {
        public static RCategory ToRCategory(this Category category)
        {
            return new RCategory
            {
                id = category.id,
                name = category.name,
                description = category.description,
                color = category.color,
                created_at = category.created_at,
            };
        }

        public static Category ToCategory(this CreateCategoryRequest category)
        {
            return new Category
            {
                name = category.name,
                description = category.description,
                color = category.color,
            };
        }

        public static Category ToCategory(this UpdateCategoryRequest category, int id)
        {
            return new Category
            {
                id = id,
                name = category.name,
                description = category.description,
                color = category.color,
            };
        }
    }
}

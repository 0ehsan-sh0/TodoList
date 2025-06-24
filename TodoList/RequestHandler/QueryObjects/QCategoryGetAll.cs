namespace TodoList.RequestHandler.QueryObjects
{
    public class QCategoryGetAll
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

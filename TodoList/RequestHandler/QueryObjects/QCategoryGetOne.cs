namespace TodoList.RequestHandler.QueryObjects
{
    public class QCategoryGetOne
    {
        public bool todos { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

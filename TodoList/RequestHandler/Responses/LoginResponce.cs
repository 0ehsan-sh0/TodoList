namespace TodoList.RequestHandler.Responces
{
    public class LoginResponce
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

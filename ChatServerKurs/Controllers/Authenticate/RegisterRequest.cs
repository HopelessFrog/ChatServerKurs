namespace ChatServerKurs.Controllers.Authenticate
{
    public class RegisterRequest
    {
        public string UserName { get; set; } = null!;
        public string LoginId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}

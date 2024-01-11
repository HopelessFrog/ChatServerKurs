namespace ChatServerKurs.Functions.User
{
    public interface IUserFunction
    {
        User? Authenticate(string loginId, string password);

        bool Register (string loginId, string password, string userName, byte[] avatarSource);
        User GetUserById(int id);
    }
}

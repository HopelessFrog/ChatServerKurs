namespace ChatServerKurs.Functions.UserFriend
{
    public interface IUserFriendFunction
    {
        Task<IEnumerable<User.User>> GetListUserFriend(int userId);
    }
}

using ChatServerKurs.Functions.User;

namespace ChatServerKurs.Controllers.Message
{
    public class MessageInitalizeResponse
    {
        public User FriendInfo { get; set; } = null!;
        public IEnumerable<Functions.Message.Message> Messages { get; set; } = null!;
    }
}

﻿using ChatServerKurs.Functions.Message;
using ChatServerKurs.Functions.User;

namespace ChatServerKurs.Controllers.ListChat
{
    public class ListChatInitializeResponse
    {
        public User User { get; set; } = null!;
        public IEnumerable<User> UserFriends { get; set; } = null!;
        public IEnumerable<LastestMessage> LastestMessages { get; set; } = null!;
    }
}

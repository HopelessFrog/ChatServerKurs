﻿namespace ChatServerKurs.Functions.User
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] AvatarSource { get; set; } = null!;
        public bool IsOnline { get; set; }
        public DateTime LastLogonTime { get; set; }
        public string Token { get; set; } = null!;
        public bool IsAway { get; set; }
        public string AwayDuration { get; set; } = null!;
    }
}

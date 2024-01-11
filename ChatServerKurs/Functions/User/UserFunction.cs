using System.IdentityModel.Tokens.Jwt;
using ChatServerKurs.Entites;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Text;
using ChatServerKurs.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace ChatServerKurs.Functions.User
{
    public class UserFunction : IUserFunction
    {
        private readonly ChatAppContext _chatAppContext;

        public UserFunction(ChatAppContext chatAppContext)
        {
            _chatAppContext = chatAppContext;
        }

        public User? Authenticate(string loginId, string password)
        {
            try
            {
                var entity = _chatAppContext.TblUsers.SingleOrDefault(x => x.LoginId == loginId);
                if (entity == null) return null;

                var isPasswordMatched = VertifyPassword(password,entity.Password);

                if (!isPasswordMatched) return null;

                var token = GenerateJwtToken(entity);

                return new User
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = token
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Register(string loginId, string password, string userName, byte[] avatarSource)
        {
            var entity = _chatAppContext.TblUsers.SingleOrDefault(x => x.LoginId == loginId);
            if (entity != null) return false;

            _chatAppContext.TblUsers.Add(new TblUser() { LoginId = loginId, Password = password, UserName = userName, AvatarSource = avatarSource });
            _chatAppContext.SaveChanges();

            _chatAppContext.TblUserFriends.RemoveRange(_chatAppContext.TblUserFriends);
            var friends = _chatAppContext.TblUsers.ToList();
            foreach (var friend in friends)
            {
                foreach (var user in friends)
                {
                    if (friend.Id != user.Id)
                    {
                        _chatAppContext.TblUserFriends.Add(
                            new TblUserFriend() { UserId = friend.Id, FriendId = user.Id });
                    }
                   
                }
               
            }

            _chatAppContext.SaveChanges();
            return true;
        }

        public User GetUserById(int id)
        {
            var entity = _chatAppContext.TblUsers
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (entity == null) return new User();

            var awayDuration = entity.IsOnline ? "" : Utilities.CalcAwayDuration(entity.LastLogonTime);
            return new User
            {
                UserName = entity.UserName,
                Id = entity.Id,
                AvatarSource = entity.AvatarSource,
                IsAway = awayDuration != "" ? true : false,
                AwayDuration = awayDuration,
                IsOnline = entity.IsOnline,
                LastLogonTime = entity.LastLogonTime
            };
        }


        private bool VertifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword.Equals(storedPassword);
        }

        private string GenerateJwtToken(TblUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("1234567890123456123456789012231345618901223134561");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}

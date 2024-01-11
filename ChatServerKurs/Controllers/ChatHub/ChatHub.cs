using ChatServerKurs.Functions.Message;
using ChatServerKurs.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace ChatServerKurs.Controllers.ChatHub
{
   
    public class ChatHub : Hub
    {

        UserOperator userOperator;
        IMessageFunction messageFunction;
        private static readonly Dictionary<int, string> _connectionMapping
            = new Dictionary<int, string>();

        public ChatHub(UserOperator userOperator, IMessageFunction messageFunction)
        {
            this.userOperator = userOperator;
            this.messageFunction = messageFunction;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(int fromUserId, int toUserId, string message)
        {
            var connectionIds = _connectionMapping.Where(x => x.Key == toUserId)
                .Select(x => x.Value).ToList();

            await messageFunction.AddMessage(fromUserId, toUserId, message);

            await Clients.Clients(connectionIds)
                .SendAsync("ReceiveMessage", fromUserId, message);
        }

        public override Task OnConnectedAsync()
        {
            SendMessage("qwe");
            var userId = userOperator.GetRequestUser().Id;
            if (!_connectionMapping.ContainsKey(userId))
                _connectionMapping.Add(userId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connectionMapping.Remove(userOperator.GetRequestUser().Id);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

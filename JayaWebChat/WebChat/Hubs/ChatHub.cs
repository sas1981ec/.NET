using Microsoft.AspNet.SignalR;

namespace WebChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string idRoom)
        {
            Clients.All.addNewMessageToPage(name, message, idRoom);
        }
    }
}
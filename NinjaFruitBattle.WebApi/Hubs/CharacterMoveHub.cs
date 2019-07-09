using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NinjaFruitBattle.WebApi.Hubs
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
    public class CharacterMoveHub : Hub
    {
        public override Task OnConnected()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            Conected(Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (UserHandler.ConnectedIds.Count > 0)
            {
                UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

        public void MovePlayer1(float x)
        {
            Clients.Others.MovePlayer1(x);
        }
        public void MovePlayer2(float x)
        {
            Clients.Others.MovePlayer2(x);
        }
        public void Conected(string connectionId)
        {
            Clients.Caller.Conected(connectionId);
        }
    }
}
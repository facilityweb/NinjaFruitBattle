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

        public void Player1MovimentaDireita(float x)
        {
            Clients.Others.Player1MovimentaDireita(x);
        }
        public void Player1MovimentaEsquerda(float x)
        {
            Clients.Others.Player1MovimentaEsquerda(x);
        }
        public void Player2MovimentaDireita(float x)
        {
            Clients.Others.Player2MovimentaDireita(x);
        }
        public void Player2MovimentaEsquerda(float x)
        {
            Clients.Others.Player2MovimentaEsquerda(x);
        }
        public void Conected(string connectionId)
        {
            Clients.Caller.Conected(connectionId);
        }
    }
}
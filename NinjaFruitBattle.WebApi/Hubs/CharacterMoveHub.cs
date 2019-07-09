using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace NinjaFruitBattle.WebApi.Hubs
{
    public class CharacterMoveHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void Move(int x)
        {
            Clients.Others.Move(x);
        }
    }
}
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace NinjaFruitBattle.WebApi.Hubs
{
    public class CharacterMoveHub : Hub
    {
        private static int _x;
        private static int _y;

        public override Task OnConnected()
        {
            Clients.Caller.UpdateState(_x, _y);
            return base.OnConnected();
        }

        public void Move(int x, int y)
        {
            _x += x;
            _y += y;
            Clients.All.UpdateState(_x, _y);
        }
    }
}
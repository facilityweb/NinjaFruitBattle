using Microsoft.AspNet.SignalR.Client;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Windows.Hub
{
    public class NinjaHub : INinjaHub
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy characterMoveHub;

        public event OnCharacterMove OnCharacterMove;
        public NinjaHub()
        {
            hubConnection = new HubConnection("http://igormonteiro-001-site3.dtempurl.com");
            characterMoveHub = hubConnection.CreateHubProxy("CharacterMoveHub");
        }
        public void Move(int xPosition)
        {
            characterMoveHub.Invoke<int>("Move", xPosition);
        }

        public void Connect()
        {
            characterMoveHub.On<int>("Move", (x) =>
            {
                OnCharacterMove?.Invoke(x);
            });
            hubConnection.Start();
        }
    }
}

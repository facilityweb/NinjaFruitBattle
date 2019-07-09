using Microsoft.AspNet.SignalR.Client;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Windows.Hub
{
    public class NinjaHub : INinjaHub
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy characterMoveHub;

        private string ConnectedId;

        public event OnPlayer1Move OnPlayer1Move;
        public event OnPlayer2Move OnPlayer2Move;
        public NinjaHub()
        {
#if DEBUG

            hubConnection = new HubConnection("http://localhost:59242/");
#endif
#if !DEBUG

            hubConnection = new HubConnection("http://igormonteiro-001-site3.dtempurl.com");
#endif

            characterMoveHub = hubConnection.CreateHubProxy("CharacterMoveHub");
        }
        public void MovePlayer1(float xPosition)
        {
            characterMoveHub.Invoke("MovePlayer1", xPosition);
        }

        public void MovePlayer2(float xPosition)
        {
            characterMoveHub.Invoke("MovePlayer2", xPosition);
        }

        public void Connect()
        {
            characterMoveHub.On<int>("MovePlayer1", (x) =>
             {
                 OnPlayer1Move?.Invoke(x);
             });
            characterMoveHub.On<int>("MovePlayer2", (x) =>
            {
                OnPlayer2Move?.Invoke(x);
            });
            characterMoveHub.On<string>("Conected", (connectedId) =>
            {
                ConnectedId = connectedId;
            });
            hubConnection.Start();
        }
    }
}

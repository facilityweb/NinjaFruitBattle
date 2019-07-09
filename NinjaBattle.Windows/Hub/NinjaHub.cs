using Microsoft.AspNet.SignalR.Client;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Windows.Hub
{
    public class NinjaHub : INinjaHub
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy characterMoveHub;

        private string ConnectedId;

        public event OnPlayer1MovimentaDireita OnPlayer1MovimentaDireita;
        public event OnPlayer1MovimentaEsquerda OnPlayer1MovimentaEsquerda;
        public event OnPlayer2MovimentaDireita OnPlayer2MovimentaDireita;
        public event OnPlayer2MovimentaEsquerda OnPlayer2MovimentaEsquerda;

        public NinjaHub()
        {
//#if DEBUG

//            hubConnection = new HubConnection("http://localhost:59242/");
//#endif
//#if !DEBUG

            hubConnection = new HubConnection("http://igormonteiro-001-site3.dtempurl.com");
//#endif

            characterMoveHub = hubConnection.CreateHubProxy("CharacterMoveHub");
        }
        public void Connect()
        {
            characterMoveHub.On<int>("Player1MovimentaDireita", (x) =>
             {
                 OnPlayer1MovimentaDireita?.Invoke(x);
             });
            characterMoveHub.On<int>("Player1MovimentaEsquerda", (x) =>
            {
                OnPlayer1MovimentaEsquerda?.Invoke(x);
            });
            characterMoveHub.On<int>("Player2MovimentaDireita", (x) =>
            {
                OnPlayer2MovimentaDireita?.Invoke(x);
            });
            characterMoveHub.On<int>("Player2MovimentaEsquerda", (x) =>
            {
                OnPlayer2MovimentaEsquerda?.Invoke(x);
            });
            characterMoveHub.On<string>("Conected", (connectedId) =>
            {
                ConnectedId = connectedId;
            });
            hubConnection.Start();
        }

        public void MovimentarPlayer1Esquerda(float xPosition)
        {
            characterMoveHub.Invoke("Player1MovimentaEsquerda", xPosition);
        }

        public void MovimentarPlayer2Esquerda(float xPosition)
        {
            characterMoveHub.Invoke("Player2MovimentaEsquerda", xPosition);
        }
        public void MovimentarPlayer1Direita(float xPosition)
        {
            characterMoveHub.Invoke("Player1MovimentaDireita", xPosition);
        }

        public void MovimentarPlayer2Direita(float xPosition)
        {
            characterMoveHub.Invoke("Player2MovimentaDireita", xPosition);
        }
    }
}

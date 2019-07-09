namespace NinjaBattle.Domain.Hub
{
    public interface INinjaHub
    {
        event OnPlayer1Move OnPlayer1Move;
        event OnPlayer2Move OnPlayer2Move;
        void MovePlayer1(float xPosition);
        void MovePlayer2(float xPosition);
        void Connect();
    }
}

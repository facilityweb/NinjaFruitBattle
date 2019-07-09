namespace NinjaBattle.Domain.Hub
{
    public interface INinjaHub
    {
        event OnCharacterMove OnCharacterMove;
        void Move(int xPosition);
        void Connect();
    }
}

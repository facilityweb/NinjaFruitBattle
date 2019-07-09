namespace NinjaBattle.Domain.Hub
{
    public interface INinjaHub
    {
        event OnPlayer1MovimentaDireita OnPlayer1MovimentaDireita;
        event OnPlayer1MovimentaEsquerda OnPlayer1MovimentaEsquerda;
        event OnPlayer2MovimentaDireita OnPlayer2MovimentaDireita;
        event OnPlayer2MovimentaEsquerda OnPlayer2MovimentaEsquerda;
        void MovimentarPlayer1Direita(float xPosition);
        void MovimentarPlayer1Esquerda(float xPosition);
        void MovimentarPlayer2Direita(float xPosition);
        void MovimentarPlayer2Esquerda(float xPosition);
        void Connect();
    }
}

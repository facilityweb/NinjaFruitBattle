using NinjaBattle.Domain.Personagens;
using System;

namespace NinjaBattle.Domain.Helper
{
    public class FimDeJogoException : Exception
    {
        public readonly TipoJogador Vencedor;
        public FimDeJogoException(TipoJogador vencedor)
        {
            this.Vencedor = vencedor;
        }
    }
}

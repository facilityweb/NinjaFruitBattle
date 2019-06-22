using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Personagens;

namespace NinjaBattle.Domain.Marcadores
{
    public class HPPlayer2 : MarcadorBase
    {
        public HPPlayer2(Game game, SpriteBatch spriteBatch, Personagem personagem) : base(game, spriteBatch)
        {
            this.LarguraPadrao = personagem.HP;
            this.LarguraPadraoBase = personagem.HP;
        }

        public override Point Posicao => new Point(Configuracao.DistanciaBarraHPX, Configuracao.DistanciaBarraHPY);

        public override Color Cor => Color.Blue;

        public override Color CorBase => Color.Red;

        public override int AlturaPadrao => Configuracao.AlturaPadraoBarra;

        public void MarcarDano(int dano)
        {
            this.AlterarMarcador(this.GetLargura() - dano);
        }
    }
}

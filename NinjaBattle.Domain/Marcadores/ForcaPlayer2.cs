using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Marcadores
{
    public class ForcaPlayer2 : MarcadorBase
    {
        public ForcaPlayer2(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            LarguraPadraoBase = Configuracao.LarguraPadraoForca;
            LarguraPadrao = 0;
        }

        public override Point Posicao => new Point(Configuracao.DistanciaBarraHPX, Configuracao.DistanciaBarraHPY + Configuracao.AlturaPadraoBarra);

        public override Color Cor => Color.Brown;

        public override Color CorBase => Color.Beige;

        public override int AlturaPadrao => Configuracao.AlturaPadraoBarra;

        public void AumentarBarraDeForca(int forca)
        {
            // calcular no eixo X invertido
            this.AlterarMarcador(forca);
        }
        public int GetForca()
        {
            return this.GetLargura();
        }
    }
}

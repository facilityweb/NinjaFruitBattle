using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Marcadores
{
    public class ForcaPlayer1 : MarcadorBase
    {
        public ForcaPlayer1(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            LarguraPadraoBase = Configuracao.LarguraPadraoForca;
            LarguraPadrao = 0;
        }

        public override Point Posicao => new Point(0, Configuracao.DistanciaBarraHPY + Configuracao.AlturaPadraoBarra);

        public override Color Cor => Color.Brown;

        public override Color CorBase => Color.Beige;

        public override int AlturaPadrao => Configuracao.AlturaPadraoBarra;

        public void AumentarBarraDeForca(int forca)
        {
            this.AlterarMarcador(Forca + forca*10);
        }
        public int Forca
        {
            get { return this.GetLargura(); }
        }
        public float ForcaConvertida
        {
            get
            {
                return this.GetLargura() / 10;
            }
        }

    }
}

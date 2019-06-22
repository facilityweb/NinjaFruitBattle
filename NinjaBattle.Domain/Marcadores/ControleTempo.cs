using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Helper;

namespace NinjaBattle.Domain.Marcadores
{
    /// <summary>
    /// Classe responsabel pelo controle do tempo no jogo
    /// </summary>
    public class ControleTempo : DrawableGameComponent
    {
        private Vector2 posicaoLabel;
        private SpriteFont font;
        private int? tempoInicial = null;
        public int TempoRestante = Configuracao.TempoPadrao;
        public ControleTempo(Game game) : base(game)
        {

        }
        public override void Update(GameTime gameTime)
        {
            ControlarTempo(gameTime.TotalGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public void ControlarTempo(double totalSeconds)
        {
            if (tempoInicial == null)
            {
                tempoInicial = (int)totalSeconds;
            }
            else
            {
                TempoRestante -= (int)(totalSeconds - tempoInicial);
            }
            if (TempoRestante <= 0)
            {
                throw new AcabouTempoException();
            }
        }
        // Fazer Draw do tempo
    }
}

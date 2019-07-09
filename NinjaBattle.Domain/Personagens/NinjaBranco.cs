using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaBranco : Personagem
    {
        public NinjaBranco(Game game, SpriteBatch spriteBatch, INinjaHub ninjaHub)
            : base(game, spriteBatch, ninjaHub) { }

        public override Color Color => Color.White;

        public override int HP => 250;

        public override double Velocidade => 2;

    }
}

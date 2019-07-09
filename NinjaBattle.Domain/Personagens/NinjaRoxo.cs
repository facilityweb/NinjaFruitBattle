using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaRoxo : Personagem
    {
        public NinjaRoxo(Game game, SpriteBatch spriteBatch, INinjaHub ninjaHub)
              : base(game, spriteBatch, ninjaHub) { }
        public override Color Color => Color.Purple;

        public override int HP => 250;

        public override double Velocidade => 5;
    }
}

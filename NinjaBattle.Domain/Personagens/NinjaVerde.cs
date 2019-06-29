using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaVerde : Personagem
    {
        public NinjaVerde(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch) { }

        public override Color Color => Color.Green;

        public override int HP => 200;

        public override double Velocidade => 3;

    }
}

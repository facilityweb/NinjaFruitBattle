using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaRoxo : Personagem
    {
        public NinjaRoxo(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch) { }
        public override Color Color => Color.Purple;

        public override int HP => 250;

        public override double Velocidade => 5;
    }
}

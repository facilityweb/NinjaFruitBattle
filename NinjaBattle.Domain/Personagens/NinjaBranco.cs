using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaBranco : Personagem
    {
        public NinjaBranco(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch) { }

        public override Color Color => Color.White;

        public override int HP => 250;

        public override double Velocidade => 2;

    }
}

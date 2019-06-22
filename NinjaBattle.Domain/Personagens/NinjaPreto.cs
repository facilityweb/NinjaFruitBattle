using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaPreto : Personagem
    {
        public NinjaPreto(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch) { }

        public override Color Color => Color.Black;


        public override int HP => 300;
        public override double Velocidade => 2;
    }
}

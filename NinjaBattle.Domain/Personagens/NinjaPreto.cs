using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaPreto : Personagem
    {
        public NinjaPreto(Game game, SpriteBatch spriteBatch, INinjaHub ninjaHub)
             : base(game, spriteBatch, ninjaHub) { }

        public override Color Color => Color.Black;


        public override int HP => 300;
        public override double Velocidade => 2;
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Hub;

namespace NinjaBattle.Domain.Personagens
{
    public class NinjaVerde : Personagem
    {
        public NinjaVerde(Game game, SpriteBatch spriteBatch, INinjaHub ninjaHub)
            : base(game, spriteBatch, ninjaHub) { }

        public override Color Color => Color.Green;

        public override int HP => 200;

        public override double Velocidade => 3;
    }
}

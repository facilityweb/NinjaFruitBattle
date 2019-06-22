using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Deck
{
    public class Deck4 : DeckBase
    {
        public Deck4(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        public override Vector2 Posicao => new Vector2(600, 0);
    }
}

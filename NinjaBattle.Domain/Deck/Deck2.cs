using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Deck
{
    public class Deck2 : DeckBase
    {
        public Deck2(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }
        public override Vector2 Posicao => new Vector2(100, 0);
    }
}

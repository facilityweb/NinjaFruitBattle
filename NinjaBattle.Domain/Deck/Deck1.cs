using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Deck
{
    public class Deck1 : DeckBase
    {
        public Deck1(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        public override Vector2 Posicao => new Vector2(0, 0);
    }
}

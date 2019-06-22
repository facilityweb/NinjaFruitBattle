using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Deck
{
    public class Deck3 : DeckBase
    {
        public Deck3(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }
        public override Vector2 Posicao => new Vector2(500, 0);
    }
}

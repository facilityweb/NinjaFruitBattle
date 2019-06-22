using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Itens
{
    public class Jaca : ItemBase
    {
        public Jaca(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        public override int Id => 3;

        public override float Massa => 2.5f;

        public override Point DanoArea => new Point(7, 7);

        public override int DanoHP => Configuracao.DanoCartaTipo4;

        public override string NomeSpriteColisao => Configuracao.NomeCartaPadraoDano;

        public override string NomeSpritePadrao => "jaca";

        public override float TempoPreparacao => Configuracao.TempoCartaTipo4;

        public override float Rotacao => 0.01f;
        public override string NomeSpriteColisaoSolo => Configuracao.ColisaoSolo4;
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Itens
{
    public class Abacaxi : ItemBase
    {
        public Abacaxi(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        public override float Massa => 0.500f;

        public override Point DanoArea => new Point(15, 15);

        public override int DanoHP => Configuracao.DanoCartaTipo2;

        public override string NomeSpriteColisao => Configuracao.NomeCartaPadraoDano;

        public override string NomeSpritePadrao => "Abacaxi";

        public override float TempoPreparacao => Configuracao.TempoCartaTipo2;
        public override int Id => 2;

        public override float Rotacao => 0.03f;

        public override string NomeSpriteColisaoSolo => Configuracao.ColisaoSolo1;
    }
}

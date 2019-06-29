using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Itens
{
    public class Maca : ItemBase
    {
        public Maca(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }
        /// <summary>
        /// uma maçã pesa mais ou menos 150 gramas
        /// </summary>
        public override float Massa => 0.150f;
        /// <summary>
        /// Diâmetro mais ou menos de 7 cm
        /// </summary>
        public override Point DanoArea => new Point(7, 7);

        public override int DanoHP => Configuracao.DanoCartaTipo1;

        public override string NomeSpriteColisao => "danoTipo1";

        public override string NomeSpritePadrao => "Maca";

        public override float TempoPreparacao => Configuracao.TempoCartaTipo1;

        public override int Id => 1;

        public override float Rotacao => 0.01f;
        public override string NomeSpriteColisaoSolo => Configuracao.ColisaoSolo1;
    }
}

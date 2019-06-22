using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Itens;

namespace NinjaBattle.Domain.Terrenos
{
    public class SoloBase : DrawableGameComponent
    {
        public Texture2D TexturaSolo;
        public Texture2D _deformacaoSolo1;
        public Texture2D _deformacaoSolo2;
        public Texture2D _deformacaoSolo3;
        public Texture2D _deformacaoSolo4;
        private readonly SpriteBatch _spriteBatch;
        public Color[,] ArrayCores;
        private uint[] pixelDeformSolo1;
        private uint[] pixelDeformSolo2;
        private uint[] pixelDeformSolo3;
        private uint[] pixelDeformSolo4;
        public SoloBase(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this._spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            TexturaSolo = Game.Content.Load<Texture2D>("Solo1");
            _deformacaoSolo1 = Game.Content.Load<Texture2D>(Configuracao.ColisaoSolo1);
            _deformacaoSolo2 = Game.Content.Load<Texture2D>(Configuracao.ColisaoSolo2);
            _deformacaoSolo3 = Game.Content.Load<Texture2D>(Configuracao.ColisaoSolo3);
            _deformacaoSolo4 = Game.Content.Load<Texture2D>(Configuracao.ColisaoSolo4);

            pixelDeformSolo1 = new uint[_deformacaoSolo1.Width * _deformacaoSolo1.Height];
            pixelDeformSolo2 = new uint[_deformacaoSolo2.Width * _deformacaoSolo2.Height];
            pixelDeformSolo3 = new uint[_deformacaoSolo3.Width * _deformacaoSolo3.Height];
            pixelDeformSolo4 = new uint[_deformacaoSolo4.Width * _deformacaoSolo4.Height];

            _deformacaoSolo1.GetData(pixelDeformSolo1, 0, _deformacaoSolo1.Width * _deformacaoSolo1.Height);
            _deformacaoSolo2.GetData(pixelDeformSolo2, 0, _deformacaoSolo2.Width * _deformacaoSolo2.Height);
            _deformacaoSolo3.GetData(pixelDeformSolo3, 0, _deformacaoSolo3.Width * _deformacaoSolo3.Height);
            _deformacaoSolo4.GetData(pixelDeformSolo4, 0, _deformacaoSolo4.Width * _deformacaoSolo4.Height);

            ArrayCores = SpriteHelper.To2DArray(TexturaSolo);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(TexturaSolo, new Vector2(0, 200), Color.White);
            base.Draw(gameTime);
        }
        public virtual Rectangle Area
        {
            get
            {
                return new Rectangle(
                   0,
                    200,
                    this.TexturaSolo.Width,
                    this.TexturaSolo.Height);
            }

        }
        public void Colidir(ItemBase item)
        {
            Texture2D currentDeformacao = null;
            uint[] currentPixel = null;

            if (item.NomeSpriteColisaoSolo == Configuracao.ColisaoSolo1)
            {
                currentDeformacao = _deformacaoSolo1;
                currentPixel = pixelDeformSolo1;
            }
            else if (item.NomeSpriteColisaoSolo == Configuracao.ColisaoSolo2)
            {
                currentDeformacao = _deformacaoSolo2;
                currentPixel = pixelDeformSolo2;
            }
            else if (item.NomeSpriteColisaoSolo == Configuracao.ColisaoSolo3)
            {
                currentDeformacao = _deformacaoSolo3;
                currentPixel = pixelDeformSolo3;
            }
            else if (item.NomeSpriteColisaoSolo == Configuracao.ColisaoSolo4)
            {
                currentDeformacao = _deformacaoSolo4;
                currentPixel = pixelDeformSolo4;
            }
            else
            {
                throw new Exception("Não implementado a colisão para esse item");
            }

            // pega posicao do solo em relação ao item
            int correcaoX = (int)item.Posicao.X - 30;

            correcaoX = correcaoX < 0 ? 0 : correcaoX;

            int posicaoY = GetPosicaoSolo((int)item.Posicao.X);
            // Declare an array to hold the pixel data 
            uint[] pixelTerreno = new uint[TexturaSolo.Width * TexturaSolo.Height];
            // Populate the array 
            TexturaSolo.GetData(pixelTerreno, 0, TexturaSolo.Width * TexturaSolo.Height);

            //pixelDeformItem = new uint[item.SpritePadrao.Width * item.SpritePadrao.Height];

            //item.SpritePadrao.GetData(pixelDeformItem, 0, item.SpritePadrao.Width * item.SpritePadrao.Height);
            for (int x = 0; x < currentDeformacao.Width; x++)
            {
                for (int y = 0; y < currentDeformacao.Height; y++)
                {
                    if (((correcaoX + x) < (TexturaSolo.Width)) && ((posicaoY + y) < (TexturaSolo.Height)))
                    {
                        if ((correcaoX + x) >= 0 && (posicaoY + y) >= 0)
                        {
                            if (currentPixel[x + y * currentDeformacao.Width] == 0)
                            {
                                continue;
                            }
                            else
                            {
                                pixelTerreno[(correcaoX + x) + ((int)posicaoY + y) * TexturaSolo.Width] = 0;
                            }
                        }
                    }
                }
            }
            TexturaSolo.SetData(pixelTerreno);
            ArrayCores = SpriteHelper.To2DArray(TexturaSolo);
        }
        private int GetPosicaoSolo(int posicaoXItem)
        {
            if (posicaoXItem >= this.TexturaSolo.Width)
                posicaoXItem = this.TexturaSolo.Width - 1;

            for (int i = 200; i < this.TexturaSolo.Height; i++)
            {
                if (this.ArrayCores[posicaoXItem, i].A != 0)
                {
                    return i - 20;
                }
            }
            return 250;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NinjaBattle.Domain.Helper
{
    /// <summary>
    /// Classe responsável por ajustar as sprites entre as sequências
    /// </summary>
    public class SpriteHelper
    {
        public static Vector2 CorrigirPosicaoArremeco(Vector2 posicaoAtual)
        {
            return new Vector2(posicaoAtual.X - 18, posicaoAtual.Y + 1);
        }
        public static float CorrigirPosicaoPersonagem(float posicao)
        {
            return posicao - 30;
        }

        public static void AlterarCor(ref Texture2D textura, Color cor)
        {
            Color[] data = new Color[textura.Width * textura.Height];
            textura.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                // cor roxa no desenho
                if (data[i].R == 142
                    && data[i].G == 24
                    && data[i].B == 115)
                {
                    data[i] = cor;
                }
            }
            textura.SetData<Color>(data);
        }

        public static void PosicionarSprite(ref Point sprite, int quantidadeEixoX, int quantidadeEixoY, Action terminouAnimacaoCallback = null)
        {
            ++sprite.X;
            if (sprite.Y >= quantidadeEixoY && sprite.X >= 1)
            {
                sprite.X = 0;
                sprite.Y = 0;
                terminouAnimacaoCallback?.Invoke();
            }
            if (sprite.X >= quantidadeEixoX)
            {
                sprite.X = 0;
                ++sprite.Y;
            }
        }

        public static void DesenharSprite(ref SpriteBatch spriteBatch, ref Texture2D TexturaPerosonagem, Vector2 posicao,
            ref Point posicaoAtual, int quantidadeImagensX, int quantidadeImagensY, SpriteEffects efeito)
        {
            spriteBatch.Draw(TexturaPerosonagem, posicao,
                new Rectangle(posicaoAtual.X * TexturaPerosonagem.Width / quantidadeImagensX,
                posicaoAtual.Y * TexturaPerosonagem.Height / quantidadeImagensY,
                TexturaPerosonagem.Width / quantidadeImagensX,
                TexturaPerosonagem.Height / quantidadeImagensY), Color.White, 0f,
            Vector2.Zero, Configuracao.EscalaPersonagem, efeito, 0f);
        }

        public static int GetLarguraSprite(Texture2D textura, int quantidadeImagensX)
        {
            return (textura.Width / quantidadeImagensX);
        }
        public static int GetAlturaSprite(Texture2D textura, int quantidadeImagensY)
        {
            return (textura.Height / quantidadeImagensY);
        }
        public static Color[,] To2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        public static bool PerPixelCollision(Texture2D item1, Rectangle area1, Texture2D item2, Rectangle area2)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[item1.Width * item1.Height];
            item1.GetData(bitsA);

            Color[] bitsB = new Color[item2.Width * item2.Height];
            item2.GetData(bitsB);

            // Calculate the intersecting rectangle
            int x1 = Math.Max(area1.X, area2.X);
            int x2 = Math.Min(area1.X + area1.Width, area2.X + area2.Width);

            int y1 = Math.Max(area1.Y, area2.Y);
            int y2 = Math.Min(area1.Y + area1.Height, area2.Y + area2.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[(x - area1.X) + (y - area1.Y) * item1.Width];
                    Color b = bitsB[(x - area2.X) + (y - area2.Y) * item2.Width];

                    if (a.A != 0 && b.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool PerPixelCollision(Color[] bitsA, Color[] bitsB, Rectangle area1, Rectangle area2, int widthA, int widthB)
        {
            // Calculate the intersecting rectangle
            int x1 = Math.Max(area1.X, area2.X);
            int x2 = Math.Min(area1.X + area1.Width, area2.X + area2.Width);

            int y1 = Math.Max(area1.Y, area2.Y);
            int y2 = Math.Min(area1.Y + area1.Height, area2.Y + area2.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[(x - area1.X) + (y - area1.Y) * widthA];
                    Color b = bitsB[(x - area2.X) + (y - area2.Y) * widthB];

                    if (a.A != 0 && b.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

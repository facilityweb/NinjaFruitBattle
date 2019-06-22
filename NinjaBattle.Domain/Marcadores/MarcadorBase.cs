using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NinjaBattle.Domain.Marcadores
{
    public abstract class MarcadorBase : DrawableGameComponent
    {
        public abstract Point Posicao { get; }
        public abstract Color Cor { get; }
        public abstract Color CorBase { get; }
        public abstract int AlturaPadrao { get; }
        public int LarguraPadraoBase { get; set; }
        public int LarguraPadrao { get; set; }
        private int _controleLargura;

        private Texture2D retangulo;
        private Texture2D retanguloBase;
        private SpriteBatch _spriteBatch;
        public MarcadorBase(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this._spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            _controleLargura = LarguraPadrao;
            if (_spriteBatch != null)
            {
                retangulo = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
                retangulo.SetData(new[] { Cor });
            }
            if (_spriteBatch != null)
            {
                retanguloBase = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
                retanguloBase.SetData(new[] { CorBase });
            }
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(retanguloBase, new Rectangle(Posicao.X, Posicao.Y, LarguraPadraoBase, AlturaPadrao), CorBase);
            _spriteBatch.Draw(retangulo, new Rectangle(Posicao.X, Posicao.Y, _controleLargura, AlturaPadrao), Cor);
            base.Draw(gameTime);
        }

        public void AlterarMarcador(int quantidade)
        {
            _controleLargura = quantidade;
        }
        public int GetLargura()
        {
            return _controleLargura;
        }
    }
}

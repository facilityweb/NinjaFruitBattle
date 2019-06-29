using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Terrenos;
using System;

namespace NinjaBattle.Domain.Itens
{
    public abstract class ItemBase : DrawableGameComponent
    {
        public Vector2 Posicao;
        public Texture2D SpritePadrao;
        private Texture2D _spriteColisao;
        private Point _spriteColidindoAtual;
        private SpriteBatch _spriteBatch;
        private SpriteEffects efeito = SpriteEffects.None;
        public StatusItem statusItem = StatusItem.Arremecando;
        public Color[,] ArrayCores;
        public abstract int Id { get; }
        /// <summary>
        /// Valor que ira ser negativado com a ação do vento ou de outro fator que envolve o eixo 
        /// </summary>
        public float ForcaY = 0;
        /// <summary>
        /// Valor que ira ser negativado com a ação do vento ou de outro fator que envolve o eixo 
        /// </summary>
        public float ForcaX = 0;
        /// <summary>
        /// Verifica se a carta está disponível para ser usada. 
        /// </summary>
        public virtual Rectangle GetArea()
        {
            return new Rectangle((int)Posicao.X, (int)Posicao.Y, (int)SpritePadrao.Width / 2, (int)SpritePadrao.Height / 2);
        }
        public bool PodeMarcarDano = true;
        public bool ColidiuSolo = false;
        //public bool Ativo
        //{
        //    get { return this.Posicao.Y < 1000; }
        //}

        public ItemBase(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._spriteColidindoAtual = new Point(0, 0);
        }
        /// <summary>
        /// Valor que ira negativar a ação da gravidade no eixo Y
        /// </summary>
        abstract public float Massa { get; }
        /// <summary>
        /// Quantidade de elementos X e Y que serão removidos ao atingir o chão
        /// </summary>
        abstract public Point DanoArea { get; }
        /// <summary>
        /// Quantidade de dano que remove ao acertar o outro personagem
        /// </summary>
        abstract public int DanoHP { get; }
        /// <summary>
        /// Nome da animação no momento da colisão
        /// </summary>
        abstract public string NomeSpriteColisao { get; }
        /// <summary>
        /// Nome da animação no momento da colisão
        /// </summary>
        abstract public string NomeSpriteColisaoSolo { get; }
        /// <summary>
        /// Nome enquanto esta em lançamento
        /// </summary>
        abstract public string NomeSpritePadrao { get; }
        /// <summary>
        /// Tempo que demora para o item ficar disponível
        /// </summary>
        abstract public float TempoPreparacao { get; }
        /// <summary>
        /// Rotação
        /// </summary>
        abstract public float Rotacao { get; }

        /// <summary>
        /// Posiciona o item quando o personagem Lancar
        /// </summary>
        /// <param name="posicaoPersonagem"> posição do personagem no momento do lançamento</param>
        public void Posicionar(Vector2 posicaoPersonagem)
        {
            this.Posicao = posicaoPersonagem;
        }
        public void MudarLadoLancamento()
        {
            efeito = SpriteEffects.FlipHorizontally;
        }
        public override void Initialize()
        {
            Posicao = new Vector2(0, 0);
            if (Game != null)
            {
                SpritePadrao = Game.Content.Load<Texture2D>(this.NomeSpritePadrao);
                _spriteColisao = Game.Content.Load<Texture2D>(this.NomeSpriteColisao);
                this.ArrayCores = SpriteHelper.To2DArray(this.SpritePadrao);
            }
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            if (statusItem == StatusItem.Arremecando)
            {
                if (efeito == SpriteEffects.None)
                {
                    Posicao.X += ForcaX;
                    Posicao.Y -= ForcaY;
                    ForcaY -= Configuracao.Gravidade;

                }
                else
                {
                    Posicao.X -= ForcaX;
                    Posicao.Y -= ForcaY;
                    ForcaY -= Configuracao.Gravidade;
                }
            }
            else if (statusItem == StatusItem.Colidindo)
            {
                SpriteHelper.PosicionarSprite(ref _spriteColidindoAtual, 10, 6, new System.Action(PararColisao));
            }
            base.Update(gameTime);
        }

        private void PararColisao()
        {
            statusItem = StatusItem.Nenhum;
        }

        public override void Draw(GameTime gameTime)
        {
            if (statusItem == StatusItem.Arremecando)
            {
                _spriteBatch.Draw(SpritePadrao, Posicao, null, Color.White, ForcaY * Rotacao, Vector2.Zero, 1, efeito, 0f);
            }
            else if (statusItem == StatusItem.Colidindo)
            {
                SpriteHelper.DesenharSprite(ref _spriteBatch, ref _spriteColisao, Posicao, ref _spriteColidindoAtual, 10, 6, efeito);
            }
            base.Draw(gameTime);
        }
        public bool Invalido
        {
            get
            {
                return Posicao.X < -100 || Posicao.X > 1500 || Posicao.Y > 1500 || this.ColidiuSolo;
            }
        }

        public bool ColideCom(SoloBase solo)
        {
            try
            {
                int widthOther = solo.TexturaSolo.Width;
                int heightOther = solo.TexturaSolo.Height;
                int widthMe = this.SpritePadrao.Width;
                int heightMe = this.SpritePadrao.Height;

                if (((Math.Min(widthOther, heightOther) > 100) ||
                    (Math.Min(widthMe, heightMe) > 100)))
                {
                    return Area.Intersects(solo.Area) && SpriteHelper.PerPixelCollision(this.SpritePadrao, this.Area, solo.TexturaSolo, solo.Area);
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public virtual Rectangle Area
        {
            get
            {
                return new Rectangle(
                    (int)this.Posicao.X - this.SpritePadrao.Width,
                    (int)this.Posicao.Y - this.SpritePadrao.Height,
                    this.SpritePadrao.Width,
                    this.SpritePadrao.Height);
            }

        }
        public void ColidirComSolo()
        {
            this.ColidiuSolo = true;
            // mudar sprite para colisao
        }
    }
}

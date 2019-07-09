using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Hub;
using NinjaBattle.Domain.Itens;
using NinjaBattle.Domain.Terrenos;
using System;
using System.Collections.Generic;

namespace NinjaBattle.Domain.Personagens
{
    public abstract class Personagem : DrawableGameComponent
    {
        abstract public Color Color { get; }
        abstract public int HP { get; }
        abstract public double Velocidade { get; }
        private double AceleracaoGravidade = Configuracao.Gravidade;
        public Vector2 Posicao;
        public Texture2D SpritePersonagemParado;
        public Texture2D SpritePersonagemAndando;
        public Texture2D SpritePersonagemLancando;
        public SpriteEffects Efeito = SpriteEffects.None;
        public IList<ItemBase> ItensLancados;
        public int ControleHP { get; set; }
        public bool IsPLayer2 { get; set; }

        private Point _spriteParadoAtual;
        private Point _spriteAndandoAtual;
        private Point _spriteLancandoAtual;
        private SpriteBatch _spriteBatch;
        private int posicaoDoNinjaNoSolo = 0;

        private INinjaHub _ninjaHub;
        public virtual Rectangle GetArea()
        {
            return new Rectangle((int)Posicao.X, (int)Posicao.Y, (int)(SpriteHelper.GetLarguraSprite(SpritePersonagemParado, 10) * Configuracao.EscalaPersonagem), (int)(SpriteHelper.GetAlturaSprite(SpritePersonagemParado, 7) * Configuracao.EscalaPersonagem));
        }

        public Personagem(Game game, SpriteBatch spriteBatch, INinjaHub ninjaHub)
            : base(game)
        {
            Posicao = new Vector2();
            ZerarParado();
            ZerarAndando();
            this._spriteBatch = spriteBatch;
            this.StatusPersonagem = StatusPersonagem.Parado;
            this.ItensLancados = new List<ItemBase>();
            ControleHP = HP;
            this._spriteLancandoAtual = Point.Zero;
            _ninjaHub = ninjaHub;
        }
        public void InicializarPlayer1()
        {
            Posicionar(Configuracao.XPlayer1, Configuracao.YPadrao);
        }
        public void InicializarPlayer2()
        {
            IsPLayer2 = true;
            Posicionar(Configuracao.XPlayer2, Configuracao.YPadrao);
        }
        public void Posicionar(int x, int y)
        {
            Posicao.X = x;
            Posicao.Y = y;
        }
        public override void Initialize()
        {
            if (Game != null)
            {
                if (!IsPLayer2)
                {
                    SpritePersonagemParado = Game.Content.Load<Texture2D>("Parado");
                    SpritePersonagemAndando = Game.Content.Load<Texture2D>("Andando");
                }
                else
                {
                    SpritePersonagemParado = Game.Content.Load<Texture2D>("Parado2");
                    SpritePersonagemAndando = Game.Content.Load<Texture2D>("Andando2");
                }
                SpritePersonagemLancando = Game.Content.Load<Texture2D>("lancando");

                if (Color != Color.Purple)
                {
                    SpriteHelper.AlterarCor(ref SpritePersonagemParado, this.Color);
                    SpriteHelper.AlterarCor(ref SpritePersonagemAndando, this.Color);
                    SpriteHelper.AlterarCor(ref SpritePersonagemLancando, this.Color);
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (StatusPersonagem == StatusPersonagem.Parado)
            {
                SpriteHelper.PosicionarSprite(ref _spriteParadoAtual, 10, 6);
                ZerarAndando();
            }
            else if (StatusPersonagem == StatusPersonagem.Andando)
            {
                SpriteHelper.PosicionarSprite(ref _spriteAndandoAtual, 10, 6);
                ZerarParado();
            }
            else if (StatusPersonagem == StatusPersonagem.Arremessando)
            {
                SpriteHelper.PosicionarSprite(ref _spriteLancandoAtual, 10, 6, new Action(TerminarLancamento));
            }

            var diferenca = (Posicao.Y - 120) - posicaoDoNinjaNoSolo;

            if ((Posicao.Y - 120) < posicaoDoNinjaNoSolo)
            {
                Posicao.Y += (float)AceleracaoGravidade;
                AceleracaoGravidade += Configuracao.Gravidade;
            }
            else if ((Posicao.Y - 120) > posicaoDoNinjaNoSolo)
            {
                Posicao.Y--;
                AceleracaoGravidade = Configuracao.Gravidade;
            }
            else
            {
                AceleracaoGravidade = Configuracao.Gravidade;
            }
            base.Update(gameTime);
        }

        private void TerminarLancamento()
        {
            _spriteLancandoAtual = Point.Zero;
            StatusPersonagem = StatusPersonagem.Parado;
            ZerarParado();
        }
        private void ZerarAndando()
        {
            _spriteAndandoAtual = new Point(0, 0);
        }
        private void ZerarParado()
        {
            _spriteParadoAtual = new Point(0, 0);
        }
        public override void Draw(GameTime gameTime)
        {
            switch (this.StatusPersonagem)
            {
                case StatusPersonagem.Parado:
                    SpriteHelper.DesenharSprite(ref _spriteBatch, ref SpritePersonagemParado, Posicao, ref _spriteParadoAtual, 10, 7, Efeito);
                    break;
                case StatusPersonagem.Andando:
                    SpriteHelper.DesenharSprite(ref _spriteBatch, ref SpritePersonagemAndando, Posicao, ref _spriteAndandoAtual, 10, 6, Efeito);
                    break;
                case StatusPersonagem.Arremessando:
                    SpriteHelper.DesenharSprite(ref _spriteBatch, ref SpritePersonagemLancando, SpriteHelper.CorrigirPosicaoArremeco(Posicao), ref _spriteLancandoAtual, 10, 6, Efeito);
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }
        public void MovimentarParaDireita()
        {
            this.StatusPersonagem = StatusPersonagem.Andando;
            this.Posicao.X += Configuracao.MovimentacaoPadrao;
        }

        public void Movimentar(int x)
        {
            this.Posicao.X = x;
        }
        public void NotificarMovimentacaoPlayer1()
        {
            _ninjaHub.MovePlayer1(this.Posicao.X);
        }
        public void NotificarMovimentacaoPlayer2()
        {
            _ninjaHub.MovePlayer2(this.Posicao.X);
        }
        public void MovimentarParaEsquerda()
        {
            this.StatusPersonagem = StatusPersonagem.Andando;
            if (this.Posicao.X > 0)
            {
                this.Posicao.X -= Configuracao.MovimentacaoPadrao;
            }
        }
        public void LancarItem(float forca, ItemBase item)
        {
            this.StatusPersonagem = StatusPersonagem.Arremessando;
            item.ForcaY = forca - item.Massa;
            item.ForcaX = forca;
            //valor chumbado para não colidir com o solo
            item.Posicao = new Vector2(this.Posicao.X, 290);
            if (Efeito != SpriteEffects.None)
                item.MudarLadoLancamento();
            ItensLancados.Add(item);
        }
        public void AplicarDano(int danoHP)
        {
            this.ControleHP -= danoHP;
            if (this.ControleHP <= 0)
            {
                if (IsPLayer2)
                {
                    throw new FimDeJogoException(TipoJogador.Player1);
                }
                else
                {
                    throw new FimDeJogoException(TipoJogador.PLayer2);
                }
            }
        }

        public void VirarEsquerda()
        {
            this.Efeito = SpriteEffects.FlipHorizontally;
        }
        public void VirarDireita()
        {
            this.Efeito = SpriteEffects.None;
        }
        public void ManterParado()
        {
            this.StatusPersonagem = StatusPersonagem.Parado;
        }
        public bool EstaLancandoItem()
        {
            return this.StatusPersonagem == StatusPersonagem.Arremessando;
        }
        public void PosicionarNoSolo(SoloBase solo)
        {
            //ajuste para posicionar corretamente no buraco
            var posicaoX = (int)this.Posicao.X + 20;
            var posicaoY = (int)this.Posicao.Y - 20;
            // partir daqui a verificação
            for (int i = 200; i < solo.TexturaSolo.Height; i++)
            {
                if (solo.ArrayCores[posicaoX, i].A != 0)
                {
                    posicaoDoNinjaNoSolo = i;
                    break;
                }
            }
        }
        public virtual bool ColideCom(ItemBase item)
        {
            try
            {
                int widthOther = item.SpritePadrao.Width;
                int heightOther = item.SpritePadrao.Height;

                int widthMe = this.GetLargura();
                int heightMe = this.GetAltura();


                if (((Math.Min(widthOther, heightOther) > 20) ||
                    (Math.Min(widthMe, heightMe) > 20)))
                {
                    return this.Area.Intersects(item.Area);
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
        public int GetLargura()
        {
            switch (this.StatusPersonagem)
            {
                case StatusPersonagem.Parado:
                    return (int)(SpriteHelper.GetLarguraSprite(SpritePersonagemParado, 10) * Configuracao.EscalaPersonagem);
                case StatusPersonagem.Andando:
                    return (int)(SpriteHelper.GetLarguraSprite(SpritePersonagemAndando, 10) * Configuracao.EscalaPersonagem);
                case StatusPersonagem.Arremessando:
                    return (int)(SpriteHelper.GetLarguraSprite(SpritePersonagemLancando, 10) * Configuracao.EscalaPersonagem);
                default:
                    break;
            }
            return 0;
        }
        public int GetAltura()
        {
            switch (this.StatusPersonagem)
            {
                case StatusPersonagem.Parado:
                    return (int)(SpriteHelper.GetAlturaSprite(SpritePersonagemParado, 7) * Configuracao.EscalaPersonagem);
                case StatusPersonagem.Andando:
                    return (int)(SpriteHelper.GetAlturaSprite(SpritePersonagemAndando, 6) * Configuracao.EscalaPersonagem);
                case StatusPersonagem.Arremessando:
                    return (int)(SpriteHelper.GetAlturaSprite(SpritePersonagemLancando, 6) * Configuracao.EscalaPersonagem);
                default:
                    break;
            }
            return 0;
        }
        public virtual Rectangle Area
        {
            get
            {
                return new Rectangle(
                    (int)this.Posicao.X - this.GetLargura(),
                    (int)this.Posicao.Y - this.GetAltura(),
                    this.GetLargura(),
                    this.GetAltura());
            }

        }
        public StatusPersonagem StatusPersonagem { get; set; }
    }
}

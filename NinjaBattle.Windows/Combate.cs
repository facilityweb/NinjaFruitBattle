using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NinjaBattle.Domain;
using NinjaBattle.Domain.Deck;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Hub;
using NinjaBattle.Domain.Itens;
using NinjaBattle.Domain.Marcadores;
using NinjaBattle.Domain.Personagens;
using NinjaBattle.Domain.Terrenos;
using System.Linq;

namespace NinjaBattle.Windows
{
    public class Combate : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _priteBatch;
        public NinjaVerde Ninja1;
        public NinjaRoxo Ninja2;
        bool gameLoop = true;
        public Deck1 Deck1;
        public Deck2 Deck2;
        public Deck3 Deck3;
        public Deck4 Deck4;

        public HPPlayer1 hPPlayer1;
        public HPPlayer2 hPPlayer2;
        public ForcaPlayer1 forcaPlayer1;
        public ForcaPlayer2 forcaPlayer2;
        public SoloBase solo;
        public Texture2D ceu;
        private KeyboardState oldKbsPlayer1;
        public Combate()
        {
            graphics = new GraphicsDeviceManager(this);
            if (Content != null)
                Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferMultiSampling = false;
        }

        public void InicializarObjetos()
        {
            // tela de escolher os personagens
            Ninja1 = new NinjaVerde(this, _priteBatch);
            Ninja1.InicializarPlayer1();
            Ninja1.Initialize();
            Ninja2 = new NinjaRoxo(this, _priteBatch);
            Ninja2.InicializarPlayer2();
            Ninja2.Initialize();
            Ninja2.VirarEsquerda();
            //inicialização dos Decks
            Deck1 = new Deck1(this, _priteBatch);
            Deck1.Initialize();
            Deck2 = new Deck2(this, _priteBatch);
            Deck2.Initialize();
            Deck3 = new Deck3(this, _priteBatch);
            Deck3.Initialize();
            Deck4 = new Deck4(this, _priteBatch);
            Deck4.Initialize();
            // inicialização dos marcadores
            hPPlayer1 = new HPPlayer1(this, _priteBatch, Ninja1);
            hPPlayer1.Initialize();

            hPPlayer2 = new HPPlayer2(this, _priteBatch, Ninja2);
            hPPlayer2.Initialize();
            forcaPlayer1 = new ForcaPlayer1(this, _priteBatch);
            forcaPlayer1.Initialize();
            forcaPlayer2 = new ForcaPlayer2(this, _priteBatch);
            forcaPlayer2.Initialize();

            solo = new SoloBase(this, _priteBatch);
            solo.Initialize();
            ceu = Content.Load<Texture2D>("ceu");
            NetworkConnection.ConnectToHub();
        }

        protected override void Initialize()
        {
            _priteBatch = new SpriteBatch(GraphicsDevice);
            InicializarObjetos();
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }
        protected override void UnloadContent()
        {
            // fazer unload Contente
            foreach (var item in Ninja1.ItensLancados.Where(x => !x.Invalido))
            {
                //item.Un(gameTime);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameLoop)
            {
                try
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                    var teclado = Keyboard.GetState();
                    if (teclado.IsKeyDown(Keys.D))
                    {
                        Ninja1.VirarDireita();
                        Ninja1.MovimentarParaDireita();
                    }
                    else if (teclado.IsKeyDown(Keys.A))
                    {
                        Ninja1.VirarEsquerda();
                        Ninja1.MovimentarParaEsquerda();
                    }
                    else if (teclado.IsKeyDown(Keys.Space))
                    {
                        if (Deck1.Disponivel && forcaPlayer1.Forca <= Configuracao.ForcaMaxima)
                        {
                            forcaPlayer1.AumentarBarraDeForca(Configuracao.Forca);
                        }
                        oldKbsPlayer1 = teclado;
                    }
                    else if (teclado.IsKeyUp(Keys.Space) && oldKbsPlayer1.IsKeyDown(Keys.Space))
                    {
                        if (Deck1.Disponivel)
                        {
                            var item = Deck1.GetItem();
                            Ninja1.LancarItem(forcaPlayer1.ForcaConvertida, item);
                            // calcular para ser uma mudança menos brusca
                            forcaPlayer1.AlterarMarcador(0);
                        }
                        oldKbsPlayer1 = teclado;
                    }
                    else
                    {
                        if (!Ninja1.EstaLancandoItem())
                        {
                            Ninja1.ManterParado();
                        }
                    }

                    Ninja1.PosicionarNoSolo(solo);
                    Ninja2.PosicionarNoSolo(solo);

                    //verificar input pressionado
                    Ninja1.Update(gameTime);
                    Ninja2.Update(gameTime);
                    //update dos Decks
                    Deck1.Update(gameTime);
                    Deck2.Update(gameTime);
                    Deck3.Update(gameTime);
                    Deck4.Update(gameTime);

                    hPPlayer1.Update(gameTime);
                    hPPlayer2.Update(gameTime);
                    forcaPlayer1.Update(gameTime);
                    forcaPlayer2.Update(gameTime);

                    // checar colisao e mudar HP
                    foreach (var item in Ninja1.ItensLancados.Where(x => !x.Invalido))
                    {
                        item.Update(gameTime);
                        if (Ninja2.ColideCom(item) && item.PodeMarcarDano)
                        {
                            hPPlayer2.MarcarDano(item.DanoHP);
                            Ninja2.AplicarDano(item.DanoHP);
                            item.PodeMarcarDano = false;
                        }
                        else if (item.ColideCom(solo))
                        {
                            solo.Colidir(item);

                            // melhorar logica pra tornar o item invalido apor ter colidido
                            item.ColidirComSolo();
                        }
                    }
                    ItemBase itemDano = null;
                    foreach (var item in Ninja1.ItensLancados.Where(x => x.statusItem == StatusItem.Nenhum))
                    {
                        itemDano = item;
                        break;
                    }
                    if (itemDano != null)
                    {
                        Ninja1.ItensLancados.Remove(itemDano);
                    }
                    base.Update(gameTime);
                }
                catch (JogadaInvalidaException)
                {
                    // mostrar jogada inválida
                }
                catch (FimDeJogoException ex)
                {
                    gameLoop = false;
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _priteBatch.Begin();
            _priteBatch.Draw(ceu, Vector2.Zero, Color.White);
            Ninja1.Draw(gameTime);
            Ninja2.Draw(gameTime);

            Deck1.Draw(gameTime);
            Deck2.Draw(gameTime);
            Deck3.Draw(gameTime);
            Deck4.Draw(gameTime);

            hPPlayer1.Draw(gameTime);
            hPPlayer2.Draw(gameTime);
            forcaPlayer1.Draw(gameTime);
            forcaPlayer2.Draw(gameTime);

            foreach (var item in Ninja1.ItensLancados.Where(x => !x.Invalido))
            {
                item.Draw(gameTime);
            }
            solo.Draw(gameTime);

            //http://web.archive.org/web/20090101215451/http://blog.xna3.com/2007/12/2d-deformable-level.html
            _priteBatch.End();
            base.Draw(gameTime);
        }
    }
}

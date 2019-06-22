//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Moq;
//using NinjaBattle.Domain;
//using NinjaBattle.Domain.Helper;
//using NinjaBattle.Domain.Itens;
//using NinjaBattle.Domain.Personagens;
//using NinjaBattle.Test.Mock;

//namespace NinjaBattle.Test
//{
//    [TestClass]
//    public class NinjaBrancoTest
//    {
//        //public static GraphicsDeviceServiceMock GraphicsDeviceMock;

//        //[AssemblyInitialize]
//        //public static void AssemblyInit(TestContext context)
//        //{
//        //    GraphicsDeviceMock = new GraphicsDeviceServiceMock();
//        //}

//        //[AssemblyCleanup]
//        //public static void AssemblyCleanup()
//        //{
//        //    GraphicsDeviceMock.Release();
//        //}
//        private Color Color => Color.White;
//        private int Movimentos => 2;
//        private int HP => 250;
//        private int Velocidade => 2;

//        [TestMethod]
//        public void HPTest()
//        {
//            var ninja = new NinjaBranco(null, null);
//            Assert.AreEqual(HP, ninja.HP);
//        }
//        [TestMethod]
//        public void VelocidadeTest()
//        {
//            var ninja = new NinjaBranco(null, null);
//            Assert.AreEqual(Velocidade, ninja.Velocidade);
//        }
//        [TestMethod]
//        public void Player1Test()
//        {
//            var ninja = new NinjaBranco(null, null);
//            ninja.InicializarPlayer1();
//            Assert.AreEqual(0, ninja.Posicao.X);
//            Assert.AreEqual(Configuracao.YPadrao, ninja.Posicao.Y);
//        }
//        [TestMethod]
//        public void Player2Test()
//        {
//            var ninja = new NinjaBranco(null, null);
//            ninja.InicializarPlayer2();
//            Assert.AreEqual(Configuracao.XPlayer2, ninja.Posicao.X);
//            Assert.AreEqual(Configuracao.YPadrao, ninja.Posicao.Y);
//        }
//        [TestMethod]
//        public void MovimentarDireitaTest()
//        {
//            var personagem = new NinjaBranco(null, null);
//            personagem.InicializarPlayer1();
//            personagem.MovimentarParaDireita();
//            Assert.AreEqual(Configuracao.XPlayer1 + Configuracao.MovimentacaoPadrao, personagem.Posicao.X);
//        }
//        [TestMethod]
//        public void VirarParaDireita()
//        {
//            var personagem = new NinjaBranco(null, null);
//            personagem.InicializarPlayer1();
//            personagem.VirarDireita();
//            Assert.AreEqual(SpriteEffects.None, personagem.Efeito);
//        }
//        [TestMethod]
//        public void VirarParaEsquerda()
//        {
//            var personagem = new NinjaBranco(null, null);
//            personagem.InicializarPlayer1();
//            personagem.VirarEsquerda();
//            Assert.AreEqual(SpriteEffects.FlipHorizontally, personagem.Efeito);
//        }
//        [TestMethod]
//        public void MovimentarEsquerdaTest()
//        {
//            var personagem = new NinjaBranco(null, null);
//            personagem.InicializarPlayer1();
//            var posicaoInicial = personagem.Posicao.X;
//            personagem.MovimentarParaDireita();
//            personagem.MovimentarParaDireita();
//            personagem.MovimentarParaEsquerda();

//            Assert.AreEqual(posicaoInicial + Configuracao.MovimentacaoPadrao, personagem.Posicao.X);
//        }
//        [TestMethod]
//        public void ArremecarItemTest()
//        {
//            var ninja = new NinjaBranco(null, null);
//            ninja.InicializarPlayer1();
//            var item = new Maca(null, null);
//            item.Posicionar(ninja.Posicao);
//            ninja.LancarItem(0.5f, item);
//            Assert.AreEqual(1, ninja.ItensLancados.Count);
//        }

//        [TestMethod]
//        public void ColisaoTest()
//        {
//            var ninjaock = new Mock<NinjaBranco>(null, null);
//            ninjaock.Setup(x => x.GetArea())
//                         .Returns(new Rectangle(10, 10, 10, 10));
//            ninjaock.Object.InicializarPlayer1();

//            var ninja2Mock = new Mock<NinjaBranco>(null,null);
//            ninja2Mock.Setup(x => x.GetArea())
//                         .Returns(new Rectangle(10, 10, 10, 10));
//            ninja2Mock.Object.InicializarPlayer2();

//            ninja2Mock.Object.ControleHP = 250;
//            var itemMock = new Mock<Maca>(null, null);
//            itemMock.Setup(x => x.GetArea())
//                        .Returns(new Rectangle((int)ninja2Mock.Object.Posicao.X, (int)ninja2Mock.Object.Posicao.Y, 5, 5));
//            itemMock.Object.Posicionar(ninja2Mock.Object.Posicao);
//             ninjaock.Object.LancarItem(0.5f, itemMock.Object);
//            itemMock.Setup(x => x.ColideCom(ninja2Mock.Object)).Returns(true);
//            if (itemMock.Object.ColideCom(ninjaock.Object))
//            {
//                Assert.Fail();
//            }
//            if (itemMock.Object.ColideCom(ninja2Mock.Object))
//            {
//                ninja2Mock.Object.ControleHP = 250;
//                ninja2Mock.Object.AplicarDano(Configuracao.DanoCartaTipo1);
//            }
//            Assert.AreEqual(250 - Configuracao.DanoCartaTipo1, ninja2Mock.Object.ControleHP);
//        }

//        [TestMethod]
//        public void PLayer1GanhouTest()
//        {
//            try
//            {
//                var ninjaock = new Mock<NinjaBranco>(null, null);
//                ninjaock.Setup(x => x.GetArea())
//                             .Returns(new Rectangle(10, 10, 10, 10));
//                ninjaock.Object.InicializarPlayer1();

//                var ninja2Mock = new Mock<NinjaBranco>(null, null);
//                ninja2Mock.Setup(x => x.GetArea())
//                             .Returns(new Rectangle(10, 10, 10, 10));
//                ninja2Mock.Object.InicializarPlayer2();

//                ninja2Mock.Object.ControleHP = 250;
//                var itemMock = new Mock<Maca>(null, null);
//                itemMock.Setup(x => x.GetArea())
//                            .Returns(new Rectangle((int)ninja2Mock.Object.Posicao.X, (int)ninja2Mock.Object.Posicao.Y, 5, 5));
//                itemMock.Object.Posicionar(ninja2Mock.Object.Posicao);
//                ninjaock.Object.LancarItem(0.5f, itemMock.Object);
//                itemMock.Setup(x => x.ColideCom(ninja2Mock.Object)).Returns(true);
//                if (itemMock.Object.ColideCom(ninjaock.Object))
//                {
//                    Assert.Fail();
//                }
//                if (itemMock.Object.ColideCom(ninja2Mock.Object))
//                {
//                    ninja2Mock.Object.ControleHP = 10;
//                    ninja2Mock.Object.AplicarDano(Configuracao.DanoCartaTipo1);
//                }
//                Assert.Fail();
//            }
//            catch (FimDeJogoException ex)
//            {
//                if (ex.Vencedor != TipoJogador.Player1)
//                {
//                    Assert.Fail();
//                }
//            }
//        }
//    }
//}

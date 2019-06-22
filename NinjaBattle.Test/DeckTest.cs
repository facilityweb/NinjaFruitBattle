using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaBattle.Domain.Personagens;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Itens;
using NinjaBattle.Windows;
using NinjaBattle.Domain;

namespace NinjaBattle.Test
{
    /// <summary>
    /// Test para Controle das cartas
    /// </summary>
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void PreparaDeckTest()
        {
            var combate = new Combate();
            combate.InicializarObjetos();
            Assert.AreNotEqual(null, combate.Deck1);
        }
        [TestMethod]
        public void DeckIndisponivelTest()
        {
            var combate = new Combate();
            combate.InicializarObjetos();
            Assert.AreNotEqual(true, combate.Deck1.Disponivel);
        }
        [TestMethod]
        public void DeckDisponivelTest()
        {
            var combate = new Combate();
            combate.InicializarObjetos();
            combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo4 * 2);
            combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo4 * 3);
            Assert.AreNotEqual(false, combate.Deck1.Disponivel);
        }
        [TestMethod]
        public void LancaDeckIndisponivel()
        {
            try
            {
                var combate = new Combate();
                combate.InicializarObjetos();
                combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo1 * 2);
                combate.Ninja1.LancarItem(1f, combate.Deck1.GetItem());
                if (combate.Ninja1.ItensLancados.Count > 0)
                {
                    Assert.Fail();
                }
            }
            catch (JogadaInvalidaException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
        [TestMethod]
        public void LancaDeckDisponivel()
        {
            var combate = new Combate();
            combate.InicializarObjetos();
            combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo1 * 2);
            combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo1 * 3);
            combate.Deck1.VerificaDisponibilidade(Configuracao.TempoCartaTipo1 * 4);
            combate.Ninja1.LancarItem(1f, combate.Deck1.GetItem());
            Assert.AreEqual(1, combate.Ninja1.ItensLancados.Count);
        }
    }
}
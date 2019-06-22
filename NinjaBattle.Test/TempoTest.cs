using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaBattle.Domain;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Marcadores;
using System;

namespace NinjaBattle.Test
{
    [TestClass]
    public class TempoTest
    {
        [TestMethod]
        public void VerificandoTempoDiminuindo()
        {
            var controle = new ControleTempo(null);
            controle.ControlarTempo(1);
            var tempoInicial = controle.TempoRestante;
            controle.ControlarTempo(2);
            Assert.AreEqual(tempoInicial - 1, controle.TempoRestante);
        }
        [TestMethod]
        public void VerificandoFimDoJogo()
        {
            try
            {
                var controle = new ControleTempo(null);
                controle.ControlarTempo(1);
                var tempoInicial = controle.TempoRestante;
                controle.ControlarTempo(Configuracao.TempoPadrao + 1);
                Assert.Fail();
            }
            catch (AcabouTempoException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}

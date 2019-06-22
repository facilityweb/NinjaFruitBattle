using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBattle.Domain.Helper;
using NinjaBattle.Domain.Itens;
using System.Collections.Generic;
using System.Linq;

namespace NinjaBattle.Domain.Deck
{
    public abstract class DeckBase : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        public abstract Vector2 Posicao { get; }
        public bool Disponivel = false;
        private float tempoInicial = 0;
        private ItemBase item;
        public IList<ItemBase> ItensDisponiveis;
        public IDictionary<int, Texture2D> texturasDeck;
        public DeckBase(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this._spriteBatch = spriteBatch;
            ItensDisponiveis = PrepararListaItens();
        }

        public override void Initialize()
        {
            texturasDeck = new Dictionary<int, Texture2D>();
            ItensDisponiveis = PrepararListaItens();
            foreach (var item in ItensDisponiveis)
            {
                if (Game != null)
                    texturasDeck.Add(item.Id, Game.Content.Load<Texture2D>(item.NomeSpritePadrao));
            }
            item = PegarItemDaLista();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Disponivel)
                VerificaDisponibilidade(gameTime.TotalGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            var texturaDeck = texturasDeck.Where(x => x.Key == item.Id).FirstOrDefault();
            if (!Disponivel)
            {
                _spriteBatch.Draw(texturaDeck.Value, Posicao, Color.Black);
            }
            else
            {
                _spriteBatch.Draw(texturaDeck.Value, Posicao, Color.White);
            }
            base.Draw(gameTime);
        }
        public void VerificaDisponibilidade(double totalSeconds)
        {
            if (tempoInicial == 0)
            {
                tempoInicial = (float)totalSeconds;
            }
            if (item.TempoPreparacao - ((float)totalSeconds - tempoInicial) <= 0)
            {
                Disponivel = true;
            }
        }
        private ItemBase PegarItemDaLista()
        {
            if (ItensDisponiveis.Count == 0)
            {
                ItensDisponiveis = PrepararListaItens();
            }
            var item = ItensDisponiveis.FirstOrDefault();
            ItensDisponiveis.Remove(item);
            item.Initialize();
            return item;
        }
        private IList<ItemBase> PrepararListaItens()
        {
            var lista = new List<ItemBase>
                        {
                         new Maca(this.Game, _spriteBatch),
                         new Abacaxi(this.Game, _spriteBatch),
                         new Jaca(this.Game, _spriteBatch),
                         new Melancia(this.Game, _spriteBatch)
                        };
            lista.Shuffle();
            return lista;
        }
        public ItemBase GetItem()
        {
            if (Disponivel)
            {
                var itemAnterior = item;
                ItensDisponiveis.Remove(item);
                item = PegarItemDaLista();
                Disponivel = false;
                tempoInicial = 0;
                return itemAnterior;
            }
            throw new JogadaInvalidaException();
        }
    }
}

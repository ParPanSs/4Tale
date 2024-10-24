using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace _4Tale
{
    [Serializable]
    public class DeckController
    {
        private DeckView _deckView;
        private DeckModel _deckModel = new();

        public void Construct(DeckView deckView, List<CardSO> startDeck, List<CardSO> foldDeck)
        {
            _deckView = deckView;
            _deckModel.SetDecks(startDeck, foldDeck);
            TakeCards(5);
        }
        
        public void TakeCards(int count)
        {
            if (count > _deckModel.GetTakeDeck().Count) 
                ShuffleDeck();
            for (var i = count - 1; i >= 0; i--)
            {
                _deckModel.RemoveCardFromDeck(_deckModel.GetTakeDeck(), _deckModel.GetTakeDeck()[i]);
            }
            _deckView.AddCardToHand(count);
            UpdateCounters();
        }

        public void FoldCardEvent(object sender, CardSO e)
        {
            FoldCard(e);
        }
        
        private void FoldCard(CardSO foldedCard)
        {
            _deckModel.AddCardToDeck(_deckModel.GetFoldDeck(), foldedCard);
            UpdateCounters();
        }

        public void ShuffleDeck()
        {
            for (int t = 0; t < _deckModel.GetFoldDeck().Count; t++ )
            {
                var tmp = _deckModel.GetFoldDeck()[t];
                int r = Random.Range(t, _deckModel.GetFoldDeck().Count);
                _deckModel.GetFoldDeck()[t] = _deckModel.GetFoldDeck()[r];
                _deckModel.GetFoldDeck()[r] = tmp;
            }

            _deckModel.SwapDecks();
            _deckModel.GetFoldDeck().Clear();
            UpdateCounters();
        }

        private void UpdateCounters()
        {
            _deckView.SetCounters(_deckModel.GetTakeDeck().Count, _deckModel.GetFoldDeck().Count);
        }
    }
}

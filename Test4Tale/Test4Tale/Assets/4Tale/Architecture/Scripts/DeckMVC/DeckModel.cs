using System.Collections.Generic;

namespace _4Tale
{
    public class DeckModel
    {
        private List<CardSO> _takeDeck = new();
        private List<CardSO> _foldDeck = new();

        public void SetDecks(List<CardSO> takeDeck, List<CardSO> foldDeck)
        {
            _takeDeck = takeDeck;
            _foldDeck = foldDeck;
        }

        public List<CardSO> GetTakeDeck()
        {
            return _takeDeck;
        }

        public List<CardSO> GetFoldDeck()
        {
            return _foldDeck;
        }

        public void RemoveCardFromDeck(List<CardSO> deck, CardSO card)
        {
            if (deck.Count == 1)
            {
                deck.Clear();
                return;
            }
            deck.Remove(card);
        }
        public void AddCardToDeck(List<CardSO> deck, CardSO card)
        {
            deck.Add(card);
        }

        public void SwapDecks()
        {
            _takeDeck.AddRange(_foldDeck);
        }
    }
}

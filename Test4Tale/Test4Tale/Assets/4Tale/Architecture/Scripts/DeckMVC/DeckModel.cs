using System.Collections.Generic;

namespace _4Tale
{
    public class DeckModel
    {
        private List<CardSO> _takeDeck;
        private List<CardSO> _foldDeck = new();

        public void SetTakeDeck(List<CardSO> takeDeck)
        {
            _takeDeck = takeDeck;
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
            deck.Remove(card);
        }
        public void AddCardToDeck(List<CardSO> deck, CardSO card)
        {
            deck.Add(card);
        }
    }
}

using System;

namespace _4Tale
{
    [Serializable]
    public class DeckController
    {
        private DeckView _deckView;

        public void Construct(DeckView deckView)
        {
            _deckView = deckView;
        }
    }
}

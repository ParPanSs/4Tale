using System;

namespace _4Tale
{
    public class CardPlayEvent
    {
        public event EventHandler<CardSO> OnCardPlayed;

        public void InvokeEvent(CardSO playedCard)
        {
            OnCardPlayed?.Invoke(this, playedCard);
        }
    }
}

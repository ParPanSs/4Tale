using System.Collections.Generic;
using UnityEngine;

namespace _4Tale
{
    public class HandVisual : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform handPosition;
        [SerializeField] private float fanSpread;
        [SerializeField] private float cardSpacing;
        [SerializeField] private float verticalSpacing;
        private List<GameObject> _cards = new();
        private List<CardDragNDrop> _cardDragNDrop = new();
        private DeckController _deckController;

        public void SetDeckController(DeckController controller)
        {
            _deckController = controller;
        }
        public void AddCardsToHand()
        {
            GameObject newCard = Instantiate(cardPrefab, handPosition.position, Quaternion.identity, handPosition);
            _cardDragNDrop.Add(newCard.GetComponent<CardDragNDrop>());
            _cards.Add(newCard);
        }

        public void UpdateHandVisuals()
        {
            int cardCount = _cards.Count;

            if (cardCount == 1)
            {
                _cards[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                _cards[0].transform.localPosition = new Vector3(0f, 0f, 0f);
                return;
            }

            for (int i = 0; i < cardCount; i++)
            {
                float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f);
                _cards[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
                float horizontalOffset = cardSpacing * (i - (cardCount - 1) / 2f);
                float normalizedPosition = 2f * i / (cardCount - 1) - 1f;
                float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
                _cards[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
            }
        }

        public void ConstructDragNDrop()
        {
            foreach (var carddnd in _cardDragNDrop)
            {
                carddnd.GetComponent<CardDragNDrop>().Construct(_deckController);
            }
        }

        public void ClearHand()
        {
            foreach (var card in _cardDragNDrop)
            {
                card.FoldCard();
            }
            _cardDragNDrop.Clear();
            _cards.Clear();
        }

        public List<CardDragNDrop> GetDragNDrop()
        {
            return _cardDragNDrop;
        }

        public List<GameObject> GetCardsList()
        {
            return _cards;
        }
    }
}

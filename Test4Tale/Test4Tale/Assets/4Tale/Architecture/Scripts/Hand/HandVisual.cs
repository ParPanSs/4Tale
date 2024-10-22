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
        [SerializeField] private List<GameObject> cards = new();
        [SerializeField] private List<CardDragNDrop> cardDragNDrop;

        private void Start()
        {
            AddCardsToHand();
            AddCardsToHand();
            AddCardsToHand();
            AddCardsToHand();
            AddCardsToHand();
            foreach (var card in cards)
            {
                cardDragNDrop.Add(card.GetComponent<CardDragNDrop>());
            }

            foreach (var card in cardDragNDrop)
            {
                card.CachePosition();
            }
        }

        private void AddCardsToHand()
        {
            GameObject newCard = Instantiate(cardPrefab, handPosition.position, Quaternion.identity, handPosition);
            cards.Add(newCard);

            UpdateHandVisuals();
        }

        private void UpdateHandVisuals()
        {
            int cardCount = cards.Count;

            if (cardCount == 1)
            {
                cards[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                cards[0].transform.localPosition = new Vector3(0f, 0f, 0f);
                return;
            }

            for (int i = 0; i < cardCount; i++)
            {
                float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f);
                cards[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
                float horizontalOffset = cardSpacing * (i - (cardCount - 1) / 2f);
                float normalizedPosition = 2f * i / (cardCount - 1) - 1f;
                float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
                cards[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
            }
        }
        
    }
}

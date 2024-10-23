using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _4Tale
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardSO[] cards;
        [SerializeField] private TextMeshProUGUI cardCost;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI cardDescription;
        [SerializeField] private Image cardSprite;
        private CardSO _selectedCard;
        [SerializeField] public CardType CardType;
        private void Start()
        {
            _selectedCard = cards[Random.Range(0, cards.Length)];
            cardCost.text = _selectedCard.cardCost.ToString();
            cardName.text = _selectedCard.cardName;
            cardDescription.text = _selectedCard.cardDescription;
            cardSprite.sprite = _selectedCard.cardSprite;
            CardType = _selectedCard.cardType;
        }
    }
}

using UnityEngine;

namespace _4Tale
{
    public enum CardType
    {
        Target,
        NonTarget,
    }
    
    [CreateAssetMenu(fileName = "Create Card", menuName = "ScriptableObject/Create Card")]
    public class CardSO : ScriptableObject
    {
        public CardType cardType;
        public int cardCost;
        public string cardName;
        [TextArea(3,5)]
        public string cardDescription;
        public Sprite cardSprite;
    }
}

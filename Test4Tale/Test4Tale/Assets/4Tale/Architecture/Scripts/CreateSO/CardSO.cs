using System;
using UnityEngine;

namespace _4Tale
{
    [Serializable]
    public enum CardType
    {
        Target,
        NonTarget,
    }

    [Serializable]
    public enum CardState
    {
        Attack,
        Heal,
        Defense,
    }
    
    [CreateAssetMenu(fileName = "Create Card", menuName = "ScriptableObject/Create Card")]
    public class CardSO : ScriptableObject
    {
        public CardState cardState;
        public CardType cardType;
        public int cardCost;
        public string cardName;
        [TextArea(3,5)]
        public string cardDescription;
        public Sprite cardSprite;
    }
}

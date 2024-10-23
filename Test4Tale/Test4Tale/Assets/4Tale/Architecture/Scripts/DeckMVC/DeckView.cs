using TMPro;
using UnityEngine;

namespace _4Tale
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI takeDeckCountText;
        [SerializeField] private TextMeshProUGUI foldDeckCountText;
        [SerializeField] private HandVisual handVisual;

        public void SetCounters(int takeDeckCount, int foldDeckCount)
        {
            takeDeckCountText.text = takeDeckCount.ToString();
            foldDeckCountText.text = foldDeckCount.ToString();
        }

        public void AddCardToHand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                handVisual.AddCardsToHand();
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _4Tale
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameFlow gameFlow;
        [SerializeField] private DeckView deckView;
        [SerializeField] private HandVisual handVisual;
        [SerializeField] private PlayerCharacteristics playerCharacteristics;
        [SerializeField] private EnemyCharacteristics[] enemyCharacteristics;
        [SerializeField] private List<CardSO> allCards;

        private List<CardSO> _startDeck = new();
        private List<CardSO> _foldDeck = new();

        private DeckController _deckController = new();

        private void Awake()
        {
            for (int i = 0; i < 15; i++)
            {
                _startDeck.Add(allCards[Random.Range(0, 3)]);
            }
            handVisual.SetDeckController(_deckController);
            playerCharacteristics.Construct();
            playerCharacteristics.SetEnergy(3);
            foreach (var enemy in enemyCharacteristics)
            {
                enemy.Construct();
            }
            gameFlow.Construct(_deckController, enemyCharacteristics);
            
            _deckController.Construct(deckView, _startDeck, _foldDeck);
        }
    }
}

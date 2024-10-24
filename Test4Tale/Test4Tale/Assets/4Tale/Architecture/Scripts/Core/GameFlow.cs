using UnityEngine;

namespace _4Tale
{
    public class GameFlow : MonoBehaviour
    {
        [SerializeField] private HandVisual handVisual;
        [SerializeField] private PlayerCharacteristics playerCharacteristics;
        private DeckController _deckController;
        private EnemyCharacteristics[] _enemyCharacteristics;
        
        public void Construct(DeckController deckController, EnemyCharacteristics[] enemyCharacteristics)
        {
            _deckController = deckController;
            _enemyCharacteristics = enemyCharacteristics;
        }
        
        private void PlayerTurn()
        {
            playerCharacteristics.SetEnergy(3);
            _deckController.TakeCards(5);
        }

        public void EndPlayerTurn()
        {
            handVisual.ClearHand();
            EnemyTurn();
        }

        private void EnemyTurn()
        {
            foreach (var enemy in _enemyCharacteristics)
            {
                enemy.SetEnemyBehaviour();
            }
            PlayerTurn();
        }
    }
}

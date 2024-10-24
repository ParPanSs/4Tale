using UnityEngine;
using UnityEngine.SceneManagement;

namespace _4Tale
{
    public class EnemyDestroyer : MonoBehaviour
    {
        [SerializeField] private EnemyCharacteristics[] enemyCharacteristics;
        private int _deadEnemyCounter;

        private void Update()
        {
            if (_deadEnemyCounter == enemyCharacteristics.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        public void DestroyEnemy()
        {
            _deadEnemyCounter++;
        }
    }
}
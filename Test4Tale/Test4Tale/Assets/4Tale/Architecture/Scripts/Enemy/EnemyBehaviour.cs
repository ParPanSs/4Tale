using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _4Tale
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyCharacteristics enemyCharacteristics;
        [SerializeField] private PlayerCharacteristics playerCharacteristics;
        private int _enemyAction;

        public void SetBehaviour(int attack, int armor)
        {
            _enemyAction = Random.Range(0, 2);
            switch (_enemyAction)
            {
                case 0:
                    EnemyAttack(attack);
                    break;
                case 1:
                    EnemyDefense(armor);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnemyAttack(int value)
        {
            playerCharacteristics.TakeDamage(value);
        }

        private void EnemyDefense(int value)
        {
            enemyCharacteristics.ArmorHP += value;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace _4Tale
{
    public class EnemyCharacteristics : MonoBehaviour, ICharacteristics
    {
        [SerializeField] private int enemyAttack;
        [SerializeField] private int enemyArmor;
        [SerializeField] private EnemyBehaviour enemyBehaviour;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        public int HP { get; set; }
        public int ArmorHP { get; set; }

        public void Construct()
        {
            HP = 10;
            ArmorHP = 0;
        }
        
        public void TakeDamage(int damage)
        {
            if (ArmorHP > 0)
            {
                ArmorHP -= damage;
                if (ArmorHP < 0)
                {
                    HP += ArmorHP;
                    ArmorHP = 0;
                }
            }
            else if (HP > 0)
            {
                HP -= damage;
            }

            if (HP <= 0)
            {
                enemyDestroyer.DestroyEnemy();
                gameObject.SetActive(false);
            }
        }

        public void SetEnemyBehaviour()
        {
            enemyBehaviour.SetBehaviour(enemyAttack, enemyArmor);
        }
    }
}

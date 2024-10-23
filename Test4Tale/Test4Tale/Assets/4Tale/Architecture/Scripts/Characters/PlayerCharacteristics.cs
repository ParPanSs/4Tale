using UnityEngine;
using UnityEngine.SceneManagement;

namespace _4Tale
{
    public class PlayerCharacteristics : MonoBehaviour, ICharacteristics
    {
        public int Energy { get; set; }
        public int HP { get; set; }
        public int ArmorHP { get; set; }

        public void Construct()
        {
            HP = 100;
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
                }
            }
            else if (HP > 0)
            {
                HP -= damage;
            }

            if (HP < 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void SetEnergy(int energy)
        {
            Energy = energy;
        }
    }
}

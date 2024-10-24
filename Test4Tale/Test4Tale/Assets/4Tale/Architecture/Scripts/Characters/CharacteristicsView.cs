using TMPro;
using UnityEngine;

namespace _4Tale
{
    public class CharacteristicsView : MonoBehaviour
    {
        [SerializeField] private PlayerCharacteristics playerCharacteristics;
        [SerializeField] private EnemyCharacteristics enemyCharacteristics;
        
        [SerializeField] private TextMeshProUGUI playerHpText;
        [SerializeField] private TextMeshProUGUI playerArmorText;
        [SerializeField] private TextMeshProUGUI playerEnergyText;
        [SerializeField] private TextMeshProUGUI[] enemyHpText;
        [SerializeField] private TextMeshProUGUI[] enemyArmorText;

        public void Update()
        {
            playerHpText.text = $"HP: {playerCharacteristics.HP}";
            playerArmorText.text = $"Armor: {playerCharacteristics.ArmorHP}";
            playerEnergyText.text = playerCharacteristics.Energy.ToString();
            for (int i = 0; i < enemyHpText.Length; i++)
            {
                enemyHpText[i].text = $"HP: {enemyCharacteristics.HP}";
                enemyArmorText[i].text = $"Armor: {enemyCharacteristics.ArmorHP}";
            }
        }
    }
}

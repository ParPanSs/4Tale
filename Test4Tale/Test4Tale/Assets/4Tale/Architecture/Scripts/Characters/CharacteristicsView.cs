using TMPro;
using UnityEngine;

namespace _4Tale
{
    public class CharacteristicsView : MonoBehaviour
    {
        [SerializeField] private PlayerCharacteristics playerCharacteristics;
        //[SerializeField] private PlayerCharacteristics enemyCharacteristics;
        
        [SerializeField] private TextMeshProUGUI playerHpText;
        [SerializeField] private TextMeshProUGUI playerArmorText;
        [SerializeField] private TextMeshProUGUI playerEnergyText;
        [SerializeField] private TextMeshProUGUI enemyHpText;

        public void Update()
        {
            playerHpText.text = $"HP: {playerCharacteristics.HP}";
            playerArmorText.text = $"Armor: {playerCharacteristics.ArmorHP}";
            playerEnergyText.text = playerCharacteristics.Energy.ToString();
            //enemyHpText.text = enemyCharacteristics.HP.ToString();
        }
    }
}

using LD58Game.GunModule;
using LD58Game.MoneyCounterModule;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class FinaleScreenState : GameState
    {
        [SerializeField] private GameObject finaleScreen;
        [SerializeField] private TextMeshProUGUI finalInventoryValueUI;
        [SerializeField] private TextMeshProUGUI leftoverMoneyUI;
        [SerializeField] private TextMeshProUGUI finalScoreUI;
        [SerializeField] private TextMeshProUGUI mostValuableGunNameUI;
        [SerializeField] private TextMeshProUGUI mostValuableGunValueUI;
        [SerializeField] private Image mostValuableGunImage;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private GunInventory gunInventory;
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip gameEndSFX;
        public override void Enter()
        {
            finaleScreen.SetActive(true);
            int finalInventoryValue = 0;
            foreach(Gun gun in gunInventory.Guns)
            {
                finalInventoryValue += gun.Value;
            }
            finalInventoryValueUI.text = $"Your final inventory value: P{finalInventoryValue}";
            leftoverMoneyUI.text = $"Your leftover money: ${moneyCounter.Money}";
            finalScoreUI.text = $"{finalInventoryValue + moneyCounter.Money}";
            string name = "None";
            int value = 0;
            if(gunInventory.Guns.Count > 0)
            {
                gunInventory.Guns.Sort((g1, g2) => g1.Value.CompareTo(g2.Value));
                name = gunInventory.Guns[^1].Name;
                value = gunInventory.Guns[^1].Value;
                mostValuableGunImage.gameObject.SetActive(true);
                mostValuableGunImage.sprite = gunInventory.Guns[^1].Icon;
            }
            mostValuableGunNameUI.text = name;
            mostValuableGunValueUI.text = $"P{value}";
            sfxPlayer.PlayOneShot(gameEndSFX);
            
        }

        public override void Exit()
        {
            finaleScreen.SetActive(false);
        }
    }
}

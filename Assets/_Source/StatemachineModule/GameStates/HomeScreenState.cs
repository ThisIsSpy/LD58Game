using LD58Game.MiscModule;
using LD58Game.MoneyCounterModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class HomeScreenState : GameState
    {
        [SerializeField] private GameObject homeScreenCanvas;
        [SerializeField] private MoneyCounter moneyCounter;

        public HomeScreenState(GameObject homeScreenCanvas)
        {
            this.homeScreenCanvas = homeScreenCanvas;
        }

        public override void Enter()
        {
            homeScreenCanvas.SetActive(true);
            moneyCounter.UpdateUI();
        }

        public override void Exit()
        {
            homeScreenCanvas.SetActive(false);
        }
    }
}

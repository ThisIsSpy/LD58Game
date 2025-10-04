using System;
using UnityEngine;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class InventoryScreenState : GameState
    {
        [SerializeField] private GameObject inventoryScreen;

        public InventoryScreenState(GameObject inventoryScreen)
        {
            this.inventoryScreen = inventoryScreen;
        }

        public override void Enter()
        {
            inventoryScreen.SetActive(true);
        }

        public override void Exit()
        {
            inventoryScreen.SetActive(false);
        }
    }
}

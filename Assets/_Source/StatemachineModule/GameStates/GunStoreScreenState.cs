using System;
using UnityEngine;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class GunStoreScreenState : GameState
    {
        [SerializeField] private GameObject gunStoreScreenCanvas;

        public GunStoreScreenState(GameObject gunStoreScreenCanvas)
        {
            this.gunStoreScreenCanvas = gunStoreScreenCanvas;
        }

        public override void Enter()
        {
            gunStoreScreenCanvas.SetActive(true);
        }

        public override void Exit()
        {
            gunStoreScreenCanvas.SetActive(false);
        }
    }
}

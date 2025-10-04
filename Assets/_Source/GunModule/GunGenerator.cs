using System.Collections.Generic;
using UnityEngine;

namespace LD58Game.GunModule
{
    public class GunGenerator : MonoBehaviour
    {
        [SerializeField] private List<GunSO> lowValueGunList;
        [SerializeField] private List<GunSO> midValueGunList;
        [SerializeField] private List<GunSO> highValueGunList;

        public GunSO GenerateGun(GunValue gunValue)
        {
            List<GunSO> gunList = new();
            switch(gunValue)
            {
                case GunValue.LowValue:
                    gunList = lowValueGunList;
                    break;
                case GunValue.MidValue:
                    gunList = midValueGunList;
                    break;
                case GunValue.HighValue:
                    gunList = highValueGunList;
                    break;
            }
            return gunList[Random.Range(0, gunList.Count - 1)];
        }
    }
}

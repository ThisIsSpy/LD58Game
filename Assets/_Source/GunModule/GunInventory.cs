using System.Collections.Generic;
using UnityEngine;

namespace LD58Game.GunModule
{
    public class GunInventory : MonoBehaviour
    {
        public List<Gun> Guns { get; private set; } = new();

        void Start()
        {
            Guns = new();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(Guns.Count == 0)
                {
                    Debug.Log("No guns.");
                    return;
                }
                foreach (Gun gun in Guns)
                {
                    Debug.Log(gun.Name);
                }
            }
        }
    }
}

using UnityEngine;

namespace LD58Game.GunModule
{
    public class Gun
    {
        public Sprite Icon { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int Value { get; private set; }

        public Gun(GunSO gunSO)
        {
            Icon = gunSO.Icon;
            Name = gunSO.Name;
            Price = gunSO.Price;
            Value = gunSO.Value;
        }
    }
}

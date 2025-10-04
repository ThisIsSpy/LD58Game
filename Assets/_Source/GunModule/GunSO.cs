using UnityEngine;

namespace LD58Game.GunModule
{
    [CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/GunSO")]
    public class GunSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LD58Game.MoneyCounterModule
{
    public class MoneyCounterUIHolder : MonoBehaviour
    {
        [field: SerializeField] public List<TextMeshProUGUI> CounterUIList { get; private set; }
    }
}

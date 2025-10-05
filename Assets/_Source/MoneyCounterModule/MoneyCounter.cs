using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace LD58Game.MoneyCounterModule
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private int startingMoney;
        private List<TextMeshProUGUI> counterUIList;
        private int money;

        public int Money
        {
            get { return money; }
            set
            {
                money = Mathf.Clamp(value, 0, int.MaxValue-1);
                UpdateUI();
                PlayerPrefs.SetInt("money", money);
            }
        }

        [Inject]
        public void Construct(MoneyCounterUIHolder uiHolder)
        {
            counterUIList = uiHolder.CounterUIList;
            Money = startingMoney;
            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (var item in counterUIList)
            {
                if (item.isActiveAndEnabled) item.text = $"${Money}";
            }
        }
    }
}

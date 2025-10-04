using LD58Game.MoneyCounterModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.GunModule
{
    public class GunCard : MonoBehaviour
    {
        [SerializeField] private Image gunImage;
        [SerializeField] private TextMeshProUGUI gunName;
        [SerializeField] private TextMeshProUGUI gunPrice;
        private MoneyCounter moneyCounter;
        private GunInventory gunInventory;
        private Button button;
        private int index;
        public GunSO GunSO { get; private set; }
        public int Price { get; private set; }

        public event System.Action<int> GunPurchased;

        public void Construct(GunSO gunSO, MoneyCounter moneyCounter, GunInventory gunInventory, int index)
        {
            button = GetComponent<Button>();
            GunSO = gunSO;
            this.moneyCounter = moneyCounter;
            this.gunInventory = gunInventory;
            this.index = index;

            gunImage.sprite = GunSO.Icon;
            gunName.text = GunSO.Name;
            Price = (int)(GunSO.Price * Random.Range(0.8f, 1.2f));
            gunPrice.text = $"${Price}";
            button.onClick.AddListener(PurchaseGun);
        }

        public void PurchaseGun()
        {
            if (Price > moneyCounter.Money)
                return;
            moneyCounter.Money -= Price;
            Gun purchasedGun = new(GunSO);
            gunInventory.Guns.Add(purchasedGun);
            GunPurchased?.Invoke(index);
        }
    }
}

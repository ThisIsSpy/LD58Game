using LD58Game.MoneyCounterModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.GunModule
{
    public class GunCard : MonoBehaviour
    {
        [SerializeField] private Image gunImage;
        [SerializeField] private Image discountMark;
        [SerializeField] private TextMeshProUGUI gunName;
        [SerializeField] private TextMeshProUGUI gunPrice;
        [SerializeField] private TextMeshProUGUI gunValue;
        private MoneyCounter moneyCounter;
        private GunInventory gunInventory;
        private Button button;
        private int index;
        public GunSO GunSO { get; private set; }
        public int Price { get; private set; }

        public event System.Action<int> GunPurchased;
        public event System.Action<int> GunSold;

        public void Construct(GunSO gunSO, MoneyCounter moneyCounter, GunInventory gunInventory, int index, PriceCalculation priceCalculation, bool isPurchasable)
        {
            button = GetComponent<Button>();
            GunSO = gunSO;
            this.moneyCounter = moneyCounter;
            this.gunInventory = gunInventory;
            this.index = index;

            gunImage.sprite = GunSO.Icon;
            gunName.text = GunSO.Name;
            switch (priceCalculation)
            {
                case PriceCalculation.Normal:
                    Price = GunSO.Price; 
                    break;
                case PriceCalculation.TwentyPercentRandom:
                    Price = (int)(GunSO.Price * Random.Range(0.8f, 1.2f));
                    break;
                case PriceCalculation.Discount:
                    Price = (int)(GunSO.Price * 0.8f);
                    discountMark.gameObject.SetActive(true);
                    break;
            }
            
            gunPrice.text = $"${Price}";
            if(isPurchasable)
                button.onClick.AddListener(PurchaseGun);
            else
                button.interactable = false;
        }

        public void Construct(Gun gun, MoneyCounter moneyCounter, int index)
        {
            button = GetComponent<Button>();
            this.moneyCounter = moneyCounter;
            button.onClick.AddListener(SellGun);
            this.index = index;
            Price = (int)(gun.Price * 0.8f);
            gunImage.sprite = gun.Icon;
            gunName.text = gun.Name;
            gunPrice.text = $"${Price}";
            gunValue.text = $"P{gun.Value}";
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

        public void SellGun()
        {
            moneyCounter.Money += Price;
            GunSold?.Invoke(index);
        }
    }

    public enum PriceCalculation
    {
        Normal = 0,
        TwentyPercentRandom = 1,
        Discount = 2,
    }
}

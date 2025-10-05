using LD58Game.GunModule;
using LD58Game.MiscModule;
using LD58Game.MoneyCounterModule;
using LD58Game.TimeCounterModule;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class GunStoreScreenState : GameState
    {
        [SerializeField] private GameObject gunStoreScreenCanvas;
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private GameObject gunCardPrefab;
        [SerializeField] private GameObject gunCardSpawnPoint;
        [SerializeField] private GunGenerator gunGenerator;
        [SerializeField] private GunInventory gunInventory;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private TimeCounter timeCounter;
        [SerializeField] private TextMeshProUGUI rewardsUI;
        [SerializeField] private Button leaveGunShopButton;
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip gunAcquiredSFX;
        [SerializeField] private AudioClip surplusGunAcquiredSFX;
        [SerializeField] private AudioClip gunNotAcquiredSFX;
        private List<GunCard> gunCards;

        public GunStoreScreenState(GameObject gunStoreScreenCanvas)
        {
            this.gunStoreScreenCanvas = gunStoreScreenCanvas;
        }

        public override void Enter()
        {
            gunStoreScreenCanvas.SetActive(true);
            leaveGunShopButton.gameObject.SetActive(true);
            moneyCounter.UpdateUI();
            leaveGunShopButton.onClick.AddListener(() => GunShopEnd(-1));
            coroutineRunner.RunCoroutine(GunShopCoroutine());
        }

        public override void Exit()
        {
            leaveGunShopButton.gameObject.SetActive(true);
            leaveGunShopButton.onClick.RemoveAllListeners();
            leaveGunShopButton.gameObject.SetActive(false);
            gunStoreScreenCanvas.SetActive(false);
        }

        public IEnumerator GunShopCoroutine()
        {
            yield return new WaitForSeconds(1f);

            gunCards = new();
            for(int i = 0; i < 3; i++)
            {
                GunSO generatedGun = i switch
                {
                    0 => gunGenerator.GenerateGun(GunValue.LowValue),
                    1 => gunGenerator.GenerateGun(GunValue.MidValue),
                    2 => gunGenerator.GenerateGun(GunValue.HighValue),
                    _ => gunGenerator.GenerateGun(GunValue.HighValue),
                };
                GameObject gunCardObject = GameObject.Instantiate(gunCardPrefab, Vector3.zero, Quaternion.identity, gunCardSpawnPoint.transform);
                GunCard gunCard = gunCardObject.GetComponent<GunCard>();
                if(UnityEngine.Random.Range(1,100) <= 60)
                    gunCard.Construct(generatedGun, moneyCounter, gunInventory, i, PriceCalculation.Normal, true);
                else
                    gunCard.Construct(generatedGun, moneyCounter, gunInventory, i, PriceCalculation.Discount, true);
                gunCards.Add(gunCard);
                gunCard.GunPurchased += GunShopEnd;
            }
        }

        public void GunShopEnd(int cardIndex)
        {
            coroutineRunner.RunCoroutine(GunShopEndCoroutine(cardIndex));
        }

        public IEnumerator GunShopEndCoroutine(int cardIndex)
        {
            leaveGunShopButton.gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                gunCards[i].GunPurchased -= GunShopEnd;
                if (i != cardIndex)
                {
                    gunCards[i].gameObject.SetActive(false);
                }
            }
            string rewards = string.Empty;
            if(cardIndex > -1)
            {
                rewards = $"You've purchased {gunCards[cardIndex].GunSO.Name} for ${gunCards[cardIndex].Price}!";
                sfxPlayer.PlayOneShot(gunAcquiredSFX);
            }
            else
            {
                if (UnityEngine.Random.Range(0, 1) == 1)
                {
                    GunSO surplusGunSO;
                    int n = UnityEngine.Random.Range(0, 2);
                    if (n == 0)
                    {
                        surplusGunSO = gunGenerator.GenerateGun(GunValue.MidValue);
                    }
                    else
                    {
                        surplusGunSO = gunGenerator.GenerateGun(GunValue.LowValue);
                    }
                    GameObject gunCardObject = GameObject.Instantiate(gunCardPrefab, Vector3.zero, Quaternion.identity, gunCardSpawnPoint.transform);
                    GunCard gunCard = gunCardObject.GetComponent<GunCard>();
                    gunCard.Construct(surplusGunSO, moneyCounter, gunInventory, -1, PriceCalculation.Normal, false);
                    gunCards.Add(gunCard);
                    rewards = $"You checked the gun surplus box on your way out and found {surplusGunSO.Name}!";
                    sfxPlayer.PlayOneShot(surplusGunAcquiredSFX);
                    Gun surplusGun = new(surplusGunSO);
                    gunInventory.Guns.Add(surplusGun);
                }
                else
                {
                    rewards = "Seems like you didn't get anything today. Better luck next time!";
                    sfxPlayer.PlayOneShot(gunNotAcquiredSFX);
                } 


            }
            rewardsUI.text = rewards;
            yield return new WaitForSeconds(4);
            foreach (var gunCard in gunCards)
            {
                GameObject.Destroy(gunCard.gameObject);
            }
            gunCards.Clear();
            rewardsUI.text = string.Empty;
            timeCounter.Time--;
            if (timeCounter.Time <= 0)
                Owner.ChangeState<FinaleScreenState>();
            else
                Owner.ChangeState<HomeScreenState>();
        }
    }
}

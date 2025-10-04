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
    public class AuctionScreenState : GameState
    {
        [SerializeField] private GameObject auctionScreenCanvas;
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private GameObject gunCardPrefab;
        [SerializeField] private GameObject gunCardSpawnPoint;
        [SerializeField] private GunGenerator gunGenerator;
        [SerializeField] private GunInventory gunInventory;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private TimeCounter timeCounter;
        [SerializeField] private TextMeshProUGUI resultsUI;
        [SerializeField] private Button leaveAuctionButton;
        private List<GunCard> gunCards;
        

        public AuctionScreenState(GameObject auctionScreenCanvas)
        {
            this.auctionScreenCanvas = auctionScreenCanvas;
        }

        public override void Enter()
        {
            auctionScreenCanvas.SetActive(true);
            moneyCounter.UpdateUI();
            leaveAuctionButton.onClick.AddListener(() => AuctionEnd(-1));
            coroutineRunner.RunCoroutine(AuctionCoroutine());
        }

        public override void Exit()
        {
            leaveAuctionButton.onClick.RemoveAllListeners();
            auctionScreenCanvas.SetActive(false);
        }

        public IEnumerator AuctionCoroutine()
        {
            yield return new WaitForSeconds(1f);
            gunCards = new();
            for (int i = 0; i < 2; i++)
            {
                GunSO generatedGun;
                int n = UnityEngine.Random.Range(1, 10);
                if (n < 7)
                    generatedGun = gunGenerator.GenerateGun(GunValue.MidValue);
                else if (n > 6 && n < 10)
                    generatedGun = gunGenerator.GenerateGun(GunValue.HighValue);
                else
                    generatedGun = gunGenerator.GenerateGun(GunValue.LowValue);
                GameObject gunCardObject = GameObject.Instantiate(gunCardPrefab, Vector3.zero, Quaternion.identity, gunCardSpawnPoint.transform);
                GunCard gunCard = gunCardObject.GetComponent<GunCard>();
                gunCard.Construct(generatedGun, moneyCounter, gunInventory, i);
                gunCards.Add(gunCard);
                gunCard.GunPurchased += AuctionEnd;
            }
        }

        public void AuctionEnd(int cardIndex)
        {
            coroutineRunner.RunCoroutine(AuctionEndCoroutine(cardIndex));
        }

        public IEnumerator AuctionEndCoroutine(int cardIndex)
        {
            Debug.Log($"index is {cardIndex}, gunCards.Count is {gunCards.Count}");
            for (int i = 0; i < 2; i++)
            {
                Debug.Log($"{i} and {gunCards[i] == null}");
                gunCards[i].GunPurchased -= AuctionEnd;
                if (i != cardIndex)
                {
                    gunCards[i].gameObject.SetActive(false);
                }
            }
            string results = string.Empty;
            if (cardIndex < 0)
                results = "Seems like you didn't get anything today. Better luck next time!";
            else
            {
                results = $"You paid ${gunCards[cardIndex].Price} for the gun. Its market price is ${gunCards[cardIndex].GunSO.Price}.";
                if (gunCards[cardIndex].Price < gunCards[cardIndex].GunSO.Price)
                    results += " You underpaid, lucky you!";
                else if (gunCards[cardIndex].Price < gunCards[cardIndex].GunSO.Price)
                    results += " You overpaid, better luck next time.";
                else
                    results += " You paid the market price.";

            }
            resultsUI.text = results;
            yield return new WaitForSeconds(4);
            foreach (var gunCard in gunCards)
            {
                GameObject.Destroy(gunCard.gameObject);
            }
            gunCards.Clear();
            resultsUI.text = string.Empty;
            timeCounter.Time--;
            Debug.Log(gunCards.Count);
            Owner.ChangeState<HomeScreenState>();
        }
    }
}

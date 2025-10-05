using LD58Game.GunModule;
using LD58Game.MiscModule;
using LD58Game.MoneyCounterModule;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class InventoryScreenState : GameState
    {
        [SerializeField] private GameObject inventoryScreen;
        [SerializeField] private GameObject gunCardSpawnPoint;
        [SerializeField] private GameObject gunCardPrefab;
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private GunInventory gunInventory;
        [SerializeField] private Button leaveInventoryButton;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private TextMeshProUGUI nothingText;
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip gunSoldSFX;
        [SerializeField] private AudioClip buttonPressSFX;
        private List<GunCard> gunCards;
        private List<GunCard> soldGunCards;

        public InventoryScreenState(GameObject inventoryScreen)
        {
            this.inventoryScreen = inventoryScreen;
        }

        public override void Enter()
        {
            inventoryScreen.SetActive(true);
            nothingText.gameObject.SetActive(false);
            leaveInventoryButton.onClick.AddListener(PreExit);
            coroutineRunner.RunCoroutine(InventoryCoroutine());
        }

        public override void Exit()
        {
            leaveInventoryButton.onClick.RemoveAllListeners();
            nothingText.gameObject.SetActive(false);
            inventoryScreen.SetActive(false);
        }

        public IEnumerator InventoryCoroutine()
        {
            yield return new WaitForSeconds(1f);
            gunCards = new();
            soldGunCards = new();
            Debug.Log(gunInventory.Guns.Count);
            if(gunInventory.Guns.Count > 0)
            {
                gunInventory.Guns.Sort((g1, g2) => g1.Value.CompareTo(g2.Value));
                for (int i = gunInventory.Guns.Count - 1; i >= 0; i--)
                {
                    if (i < 0 || i >= gunInventory.Guns.Count) break;
                    GameObject gunCardObject = GameObject.Instantiate(gunCardPrefab, Vector3.zero, Quaternion.identity, gunCardSpawnPoint.transform);
                    GunCard gunCard = gunCardObject.GetComponent<GunCard>();
                    gunCard.Construct(gunInventory.Guns[i], moneyCounter, i);
                    gunCard.GunSold += OnGunSold;
                    gunCards.Add(gunCard);
                }
            }
            else
                nothingText.gameObject.SetActive(true);

        }

        public void OnGunSold(int index)
        {
            sfxPlayer.PlayOneShot(gunSoldSFX);
            gunCards[index].GunSold -= OnGunSold;
            gunCards[index].gameObject.SetActive(false);
            soldGunCards.Add(gunCards[index]);
            gunInventory.Guns.RemoveAt(index);
            gunCards.RemoveAt(index);
            for (int i = gunInventory.Guns.Count - 1; i >= 0; i--)
            {
                if (gunCards[i] == null) continue;
                gunCards[i].Index = i;
            }
            if (gunInventory.Guns.Count == 0)
                nothingText.gameObject.SetActive(true);
        }

        public void PreExit()
        {
            sfxPlayer.PlayOneShot(buttonPressSFX);
            foreach(var gun in gunCards)
            {
                GameObject.Destroy(gun.gameObject);
            }
            foreach(var gun in soldGunCards)
            {
                GameObject.Destroy(gun.gameObject);
            }
            gunCards.Clear();
            soldGunCards.Clear();
            Owner.ChangeState<HomeScreenState>();
        }
    }
}

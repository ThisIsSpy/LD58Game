using LD58Game.GunModule;
using LD58Game.MiscModule;
using LD58Game.MoneyCounterModule;
using LD58Game.TimeCounterModule;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    [Serializable]
    public class ScrapyardScreenState : GameState
    {
        [SerializeField] private GameObject scrapyardScreenCanvas;
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private GunGenerator gunGenerator;
        [SerializeField] private GunInventory gunInventory;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private TimeCounter timeCounter;
        [SerializeField] private TextMeshProUGUI moneyRecievedText;
        [SerializeField] private TextMeshProUGUI rewardsRecievedText;
        [SerializeField] private Image gunRecievedIcon;
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip gunAcquiredSFX;

        public ScrapyardScreenState(GameObject scrapyardScreenCanvas)
        {
            this.scrapyardScreenCanvas = scrapyardScreenCanvas;
        }

        public override void Enter()
        {
            scrapyardScreenCanvas.SetActive(true);
            coroutineRunner.RunCoroutine(ScrapyardCoroutine());
        }

        public override void Exit()
        {
            scrapyardScreenCanvas.SetActive(false);
            moneyRecievedText.gameObject.SetActive(false);
            rewardsRecievedText.gameObject.SetActive(false);
            gunRecievedIcon.gameObject.SetActive(false);
        }

        public IEnumerator ScrapyardCoroutine()
        {
            yield return new WaitForSeconds(1f);

            int mult = Mathf.Clamp(timeCounter.TimePassed, 1, int.MaxValue-1);
            int baseEarnings = 100 * mult;
            int additionalEarnings = UnityEngine.Random.Range(1, 100);
            moneyRecievedText.text = $"+${baseEarnings + additionalEarnings}";

            int n = UnityEngine.Random.Range(1, 10);
            GunSO gunRecievedSO;
            if (n < 7)
                gunRecievedSO = gunGenerator.GenerateGun(GunValue.LowValue);
            else if (n > 6 && n < 10)
                gunRecievedSO = gunGenerator.GenerateGun(GunValue.MidValue);
            else
                gunRecievedSO = gunGenerator.GenerateGun(GunValue.HighValue);
            gunRecievedIcon.sprite = gunRecievedSO.Icon;

            rewardsRecievedText.text = $"You have recieved the {gunRecievedSO.Name} and ${baseEarnings + additionalEarnings}!";
            sfxPlayer.PlayOneShot(gunAcquiredSFX);
            moneyCounter.Money += baseEarnings + additionalEarnings;
            Gun newGun = new(gunRecievedSO);
            gunInventory.Guns.Add(newGun);

            moneyRecievedText.gameObject.SetActive(true);
            rewardsRecievedText.gameObject.SetActive(true);
            gunRecievedIcon.gameObject.SetActive(true);

            yield return new WaitForSeconds(4);
            timeCounter.Time--;
            if(timeCounter.Time <= 0)
                Owner.ChangeState<FinaleScreenState>();
            else
                Owner.ChangeState<HomeScreenState>();
        }
    }
}

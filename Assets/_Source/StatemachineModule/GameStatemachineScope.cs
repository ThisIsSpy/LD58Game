using LD58Game.MiscModule;
using LD58Game.MoneyCounterModule;
using LD58Game.TimeCounterModule;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace LD58Game.StatemachineModule
{
    public class GameStatemachineScope : LifetimeScope
    {
        [SerializeField] private HomeScreenState homeScreenState;
        [SerializeField] private ScrapyardScreenState scrapyardScreenState;
        [SerializeField] private AuctionScreenState auctionScreenState;
        [SerializeField] private GunStoreScreenState gunStoreScreenState;
        [SerializeField] private InventoryScreenState inventoryScreenState;
        [SerializeField] private FinaleScreenState finaleScreenState;
        [SerializeField] private Image fadeInOutImage;
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private TimeCounter timeCounter;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(homeScreenState);
            builder.RegisterInstance(scrapyardScreenState);
            builder.RegisterInstance(auctionScreenState);
            builder.RegisterInstance(gunStoreScreenState);
            builder.RegisterInstance(inventoryScreenState);
            builder.RegisterInstance(finaleScreenState);
            builder.RegisterInstance(fadeInOutImage);
            builder.RegisterInstance(coroutineRunner);
            builder.RegisterInstance(moneyCounter);
            builder.RegisterInstance(timeCounter);

            builder.Register<GameStatemachine<GameState>>(Lifetime.Singleton);
        }
    }
}

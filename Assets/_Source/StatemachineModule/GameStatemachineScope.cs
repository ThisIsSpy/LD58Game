using LD58Game.MiscModule;
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
        [SerializeField] private Image fadeInOutImage;
        [SerializeField] private CoroutineRunner coroutineRunner;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(homeScreenState);
            builder.RegisterInstance(scrapyardScreenState);
            builder.RegisterInstance(auctionScreenState);
            builder.RegisterInstance(gunStoreScreenState);
            builder.RegisterInstance(fadeInOutImage);
            builder.RegisterInstance(coroutineRunner);

            builder.Register<GameStatemachine<GameState>>(Lifetime.Singleton);
        }
    }
}

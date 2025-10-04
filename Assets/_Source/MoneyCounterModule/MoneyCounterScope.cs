using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LD58Game.MoneyCounterModule
{
    public class MoneyCounterScope : LifetimeScope
    {
        [SerializeField] private MoneyCounter moneyCounter;
        [SerializeField] private MoneyCounterUIHolder counterUIHolder;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(counterUIHolder);
            builder.RegisterComponent(moneyCounter);
        }
    }
}

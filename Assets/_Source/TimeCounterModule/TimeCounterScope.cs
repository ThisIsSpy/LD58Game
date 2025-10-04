using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LD58Game.TimeCounterModule
{
    public class TimeCounterScope : LifetimeScope
    {
        [SerializeField] private TimeCounter timeCounter;
        [SerializeField] private TextMeshProUGUI counterUI;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(timeCounter);
            builder.RegisterInstance(counterUI);
        }
    }
}

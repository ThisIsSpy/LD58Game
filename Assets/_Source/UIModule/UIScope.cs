using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LD58Game.UIModule
{
    public class UIScope : LifetimeScope
    {
        [SerializeField] private StateSwitchButtonHolder buttonHolder;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(buttonHolder);
        }
    }
}

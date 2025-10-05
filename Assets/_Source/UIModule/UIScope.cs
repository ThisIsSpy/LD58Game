using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LD58Game.UIModule
{
    public class UIScope : LifetimeScope
    {
        [SerializeField] private StateSwitchButtonHolder buttonHolder;
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioClip buttonPressedSFX;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(buttonHolder);
            builder.RegisterInstance(sfxPlayer);
            builder.RegisterInstance(buttonPressedSFX);
        }
    }
}

using LD58Game.StatemachineModule;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace LD58Game.UIModule
{
    public class StateSwitchButtonHolder : MonoBehaviour
    {
        [SerializeField] private List<StateSwitchButton> buttons;

        [Inject]
        public void Construct(GameStatemachine<GameState> gameStatemachine)
        {
            foreach(StateSwitchButton button in buttons)
            {
                button.Construct(gameStatemachine);
            }
        }
    }
}

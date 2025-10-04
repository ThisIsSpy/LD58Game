using LD58Game.StatemachineModule;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace LD58Game.UIModule
{
    public class StateSwitchButton : MonoBehaviour
    {
        [SerializeField] private GameStateType gameStateType;
        private GameStatemachine<GameState> gameStatemachine;
        private Button button;

        [Inject]
        public void Construct(GameStatemachine<GameState> gameStatemachine)
        {
            this.gameStatemachine = gameStatemachine;
            button = GetComponent<Button>();
            button.onClick.AddListener(SwitchCanvas);
        }

        void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SwitchCanvas()
        {
            switch (gameStateType)
            {
                case GameStateType.HomeScreenState:
                    gameStatemachine.ChangeState<HomeScreenState>();
                    break;
                case GameStateType.ScrapyardScreenState:
                    gameStatemachine.ChangeState<ScrapyardScreenState>();
                    break;
                case GameStateType.AuctionScreenState:
                    gameStatemachine.ChangeState<AuctionScreenState>();
                    break;
                case GameStateType.GunStoreScreen:
                    gameStatemachine.ChangeState<GunStoreScreenState>();
                    break;
            }
        }
    }
}
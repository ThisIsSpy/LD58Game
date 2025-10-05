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
        private AudioSource sfxSource;
        private AudioClip buttonPressedSFX;

        [Inject]
        public void Construct(GameStatemachine<GameState> gameStatemachine, AudioSource sfxSource, AudioClip buttonPressedSFX)
        {
            this.gameStatemachine = gameStatemachine;
            this.sfxSource = sfxSource;
            this.buttonPressedSFX = buttonPressedSFX;
            button = GetComponent<Button>();
            button.onClick.AddListener(SwitchCanvas);
        }

        void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SwitchCanvas()
        {
            sfxSource.PlayOneShot(buttonPressedSFX);
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
                case GameStateType.InventoryScreen:
                    gameStatemachine.ChangeState<InventoryScreenState>();
                    break;
            }
        }
    }
}
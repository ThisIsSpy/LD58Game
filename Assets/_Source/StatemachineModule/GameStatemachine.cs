using LD58Game.MiscModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD58Game.StatemachineModule
{
    public class GameStatemachine<T> : IStatemachine where T : GameState
    {
        private readonly Dictionary<Type, T> _states;
        private T _currentState;
        private readonly CoroutineRunner coroutineRunner;
        private readonly Image fadeInOutImage;

        public GameStatemachine(HomeScreenState homeScreenState, ScrapyardScreenState scrapyardScreenState, AuctionScreenState auctionScreenState, GunStoreScreenState gunStoreScreenState,
            CoroutineRunner coroutineRunner, Image fadeInOutImage)
        {
            _states = new()
            {
                {typeof(HomeScreenState), homeScreenState as T},
                {typeof(ScrapyardScreenState), scrapyardScreenState as T},
                {typeof(AuctionScreenState), auctionScreenState as T},
                {typeof(GunStoreScreenState), gunStoreScreenState as T}
            };
            this.coroutineRunner = coroutineRunner;
            this.fadeInOutImage = fadeInOutImage;

            InitStates();
            scrapyardScreenState.Exit();
            auctionScreenState.Exit();
            gunStoreScreenState.Exit();
            ChangeState<HomeScreenState>();
        }

        public void Update()
        {
            _currentState?.Update();
        }
        private void InitStates()
        {
            foreach (var states in _states)
            {
                states.Value.InjectOwner(this);
            }
        }
        public bool ChangeState<T1>() where T1 : GameState
        {
            //if (_states.ContainsKey(typeof(T)))
            //{
            //    _currentState?.Exit();
            //    _currentState = _states[typeof(T)];
            //    _currentState.Enter();
            //    return true;
            //}
            //return false;
            coroutineRunner.RunCoroutine(ChangeStateCoroutine<T1>());
            return true;
        }

        public IEnumerator ChangeStateCoroutine<T2>() where T2 : GameState
        {
            if (_states.ContainsKey(typeof(T2)))
            {
                fadeInOutImage.gameObject.SetActive(true);
                while (fadeInOutImage.color.a < 1)
                {
                    fadeInOutImage.color = new Color(0, 0, 0, fadeInOutImage.color.a + 0.05f);
                    yield return new WaitForSeconds(0.075f);
                }
                _currentState?.Exit();
                _currentState = _states[typeof(T2)];
                _currentState.Enter();
                while (fadeInOutImage.color.a > 0)
                {
                    fadeInOutImage.color = new Color(0, 0, 0, fadeInOutImage.color.a - 0.05f);
                    yield return new WaitForSeconds(0.075f);
                }
                fadeInOutImage.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}

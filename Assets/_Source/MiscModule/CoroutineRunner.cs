using System.Collections;
using UnityEngine;

namespace LD58Game.MiscModule
{
    public class CoroutineRunner : MonoBehaviour
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}

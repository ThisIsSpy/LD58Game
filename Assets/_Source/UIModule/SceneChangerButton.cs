using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD58Game.UIModule
{
    public class SceneChangerButton : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

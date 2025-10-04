using TMPro;
using UnityEngine;
using VContainer;

namespace LD58Game.TimeCounterModule
{
    public class TimeCounter : MonoBehaviour
    {
        private TextMeshProUGUI counterUI;
        private int time;
        private int initialTime;

        public int TimePassed { get { return initialTime - time; } }

        public int Time
        {
            get { return time; }
            set
            {
                time = Mathf.Clamp(value, 0, int.MaxValue-1);
                counterUI.text = $"{time} Days Left";
            }
        }

        [Inject]
        public void Construct(TextMeshProUGUI counterUI)
        {
            this.counterUI = counterUI;
            initialTime = PlayerPrefs.GetInt("time", 10);
            Time = initialTime;
        }
    }
}

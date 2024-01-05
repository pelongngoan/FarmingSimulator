
using UnityEngine;
using TMPro;
namespace WorldTime
{
    [RequireComponent(typeof(TMP_Text))]
    public class WorldTimeDisplay : MonoBehaviour
    {
        public WorldTime worldTime;
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }
        private void OnDestroy()
        {
            worldTime.WorldTimeChanged -= OnWorldTimeChanged;

        }
        private void OnWorldTimeChanged(object sender, System.TimeSpan newTime)
        {
            text.SetText(newTime.ToString(@"hh\:mm"));
        }
    }
}

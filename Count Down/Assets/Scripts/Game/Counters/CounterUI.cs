using TMPro;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(Canvas))]
    public class CounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void UpdateValue(string value)
        {
            _counterText.SetText(value);
        }
    }
}

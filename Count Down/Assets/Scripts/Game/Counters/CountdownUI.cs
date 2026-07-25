using TMPro;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(Canvas))]
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void SetValue(int value)
        {
            _countdownText.SetText($" {value}");
        }
    }
}

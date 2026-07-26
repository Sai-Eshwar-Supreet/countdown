using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(Canvas))]
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;

        public void Show(int startValue)
        {
            _countdownText.SetText($"{startValue}");
        }

        public void Hide()
        {
            _countdownText.SetText($"X");
        }

        public void UpdateUI(int value)
        {
            _countdownText.SetText($"{value}");
        }
    }
}

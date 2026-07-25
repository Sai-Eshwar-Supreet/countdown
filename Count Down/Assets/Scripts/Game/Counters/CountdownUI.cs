using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(Canvas))]
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private Countdown _countdown;
        [SerializeField] private TextMeshProUGUI _countdownText;

        private void Awake()
        {
            _countdown.OnStarted += Show;
            _countdown.OnTick += UpdateUI;
            _countdown.OnExpired += Hide;
        }

        private void OnDestroy()
        {
            if (_countdown == null) return;

            _countdown.OnStarted -= Show;
            _countdown.OnTick -= UpdateUI;
            _countdown.OnExpired -= Hide;
        }

        private void Show(int startValue)
        {
            _countdownText.SetText($" {startValue}");
        }

        private void Hide()
        {
            _countdownText.SetText("X");
        }

        private void UpdateUI(int value)
        {
            _countdownText.SetText($" {value}");
        }
    }
}

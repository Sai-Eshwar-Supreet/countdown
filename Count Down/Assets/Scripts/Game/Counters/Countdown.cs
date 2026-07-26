using System;
using UnityEngine;

namespace CountDown.Game
{
    public class Countdown : MonoBehaviour
    {
        public event Action<int> OnStarted;
        public event Action<int> OnTick;
        public event Action OnExpired;

        [SerializeField] private int _startValue = 10;
        [SerializeField] private CountdownUI _countdownUI;

        private bool _isRunning = true;
        private int _currentValue = 0;
        private int _carry = 0;

        private void Awake()
        {
            Restart();
        }

        private void OnEnable()
        {
            TurnManager.Instance.OnTurn += Tick;
        }
        private void OnDisable()
        {
            if (TurnManager.Instance == null) return;
            TurnManager.Instance.OnTurn -= Tick;
        }
        
        public void Restart()
        {
            _currentValue = _startValue - _carry;
            _carry = 0;

            _isRunning = true;
            _countdownUI.Show(_currentValue);
            OnStarted?.Invoke( _currentValue );
        }

        private void Tick(int cost)
        {
            if (!_isRunning) return;

            _currentValue -= cost;

            if (_currentValue <= 0)
            {
                _carry = Mathf.Abs(_currentValue) % _startValue;
                _isRunning = false;
                _countdownUI.Hide();
                OnExpired?.Invoke();
                return;
            }

            _countdownUI.UpdateUI(_currentValue);
            OnTick?.Invoke(_currentValue);
        }
    }
}

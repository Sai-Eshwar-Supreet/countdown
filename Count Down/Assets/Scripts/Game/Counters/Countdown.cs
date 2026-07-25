using System;
using UnityEngine;

namespace CountDown.Game
{
    public class Countdown : MonoBehaviour
    {
        public event Action OnExpired;

        [SerializeField] private int _startValue = 10;
        [SerializeField] private CountdownUI _countdownUI;

        private bool _isRunning = true;
        private int _currentValue = 0;

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
            _isRunning = true;
            _currentValue = _startValue;

            RefreshUI();
        }

        private void Tick()
        {
            if (!_isRunning) return;


            _currentValue--;


            if (_currentValue <= 0)
            {
                _isRunning = false;
                RefreshUI();
                OnExpired?.Invoke();
                return;
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            _countdownUI.SetVisible(_isRunning);
            _countdownUI.SetValue(_currentValue);
        }
    }
}

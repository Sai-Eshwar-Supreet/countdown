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
            OnStarted?.Invoke( _currentValue );
        }

        private void Tick(int cost)
        {
            if (!_isRunning) return;


            _currentValue -= cost;

            if (_currentValue <= 0)
            {
                _isRunning = false;
                OnExpired?.Invoke();
                return;
            }

            OnTick?.Invoke(_currentValue);
        }
    }
}

using System;
using UnityEngine;

namespace CountDown.Game
{
    public class Counter : MonoBehaviour
    {
        public event Action OnCounterEnded;

        [SerializeField] private int _startCount = 10;
        [SerializeField] private CounterUI _counterUI;

        private bool _active = true;

        private int _currentCount = 0;

        private void Awake()
        {
            ResetCounter();
        }

        private void OnEnable()
        {
            TurnManager.Instance.OnTurnPassed += OnTurnPassed;
        }
        private void OnDisable()
        {
            if (TurnManager.Instance == null) return;
            TurnManager.Instance.OnTurnPassed -= OnTurnPassed;
        }
        
        public void ResetCounter()
        {
            _active = true;
            _currentCount = _startCount;
            _counterUI.SetActive(_active);
            _counterUI.UpdateValue(_currentCount.ToString());
        }

        private void OnTurnPassed()
        {
            if (!_active) return;
            _currentCount--;
            _counterUI.UpdateValue(_currentCount.ToString());


            if (_currentCount <= 0)
            {
                _active = false;
                _counterUI.SetActive(_active);
                OnCounterEnded?.Invoke();
            }
        }
    }
}

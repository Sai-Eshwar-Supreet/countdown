using System;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown.Game
{
    public class OneShotMover : MonoBehaviour
    {
        [SerializeField] private Countdown _countdown;

        [Header("One Shot Mover Settings")]
        [SerializeField] private Transform _movableTransform;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;

        public UnityEvent OnMoved;

        private void Awake()
        {
            _movableTransform.localPosition = _startPosition;
        }


        private void OnEnable()
        {
            _countdown.OnExpired += HandleExpired;
        }

        private void OnDisable()
        {
            if (_countdown == null) return;
            _countdown.OnExpired -= HandleExpired;
        }

        private void HandleExpired()
        {
            if (_movableTransform.localPosition == _endPosition) return;

            _movableTransform.localPosition = _endPosition;
            OnMoved?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            if (_movableTransform == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + _startPosition, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _endPosition, 0.1f);
        }
    }
}

using UnityEngine;

namespace CountDown.Game
{
    public class OneShotMover : MonoBehaviour
    {
        [SerializeField] private Countdown _countdown;

        [Header("One Shot Mover Settings")]
        [SerializeField] private Transform _movableTransform;
        [SerializeField] private Collider _collider;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;

        private void Awake()
        {
            _movableTransform.localPosition = _startPosition;
            _collider.enabled = false;
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

            _collider.enabled = true;
            _movableTransform.localPosition = _endPosition;
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

using UnityEngine;

namespace CountDown.Game
{
    public class TwoStateMover : MonoBehaviour
    {
        [SerializeField] private Countdown _countdown;

        [Header("Two State Mover Settings")]
        [SerializeField] private bool _startActive = true;
        [SerializeField] private Transform _movableTransform;
        [SerializeField] private Collider _collider;
        [SerializeField] private Vector3 _activePosition;
        [SerializeField] private Vector3 _inactivePosition;

        private bool _isActive = false;

        private void Awake()
        {
            _isActive = _startActive;
            

            ApplyState();
        }


        private void OnEnable()
        {
            _countdown.OnExpired += ToggleState;
        }

        private void OnDisable()
        {
            if (_countdown == null) return;
            _countdown.OnExpired -= ToggleState;
        }

        private void ToggleState()
        {
            _isActive = !_isActive;
            ApplyState();
            _countdown.Restart();
        }

        private void ApplyState()
        {
            _movableTransform.localPosition =
                _isActive ? _activePosition : _inactivePosition;

            _collider.gameObject.SetActive(_isActive);
        }

        private void OnDrawGizmosSelected()
        {
            if (_movableTransform == null) return;

            Vector3[] worldPositions = new Vector3[2];

            transform.TransformPoints(new [] { _activePosition, _inactivePosition }, worldPositions);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(worldPositions[0], 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldPositions[1], 0.1f);
        }
    }
}

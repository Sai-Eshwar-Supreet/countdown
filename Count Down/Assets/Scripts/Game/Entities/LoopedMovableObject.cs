using DG.Tweening;
using UnityEngine;

namespace CountDown.Game
{
    public class LoopedMovableObject : MonoBehaviour
    {
        [SerializeField] private Counter _counter;

        [Header("LoopedMovableObject Settings")]
        [SerializeField] private bool _initLockedState = false;
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private Collider _collider;
        [SerializeField] private Vector3 _unlockedPosition;
        [SerializeField] private Vector3 _lockedPosition;

        private bool _locked = false;

        private void Awake()
        {
            _locked = _initLockedState;
            _doorTransform.localPosition = _locked ? _lockedPosition : _unlockedPosition;
            _collider.gameObject.SetActive(_locked);
        }


        private void OnEnable()
        {
            _counter.OnCounterEnded += OnCounterEnded;
        }

        private void OnDisable()
        {
            if (_counter == null) return;
            _counter.OnCounterEnded -= OnCounterEnded;
        }

        private void OnCounterEnded()
        {
            var targetPos = _locked ? _unlockedPosition : _lockedPosition;
            if (_doorTransform.localPosition == targetPos) return;

            _locked = !_locked;

            _counter.ResetCounter();

            _collider.gameObject.SetActive(_locked);

            _doorTransform.localPosition = targetPos;
        }

        private void OnDrawGizmosSelected()
        {
            if (_doorTransform == null) return;

            var worldPositions = new Vector3[2];

            transform.TransformPoints(new [] { _unlockedPosition, _lockedPosition }, worldPositions);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(worldPositions[0], 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldPositions[1], 0.1f);
        }
    }
}

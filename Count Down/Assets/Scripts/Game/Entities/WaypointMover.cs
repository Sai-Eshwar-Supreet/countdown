using UnityEditor;
using UnityEngine;

namespace CountDown.Game
{
    public class WaypointMover : MonoBehaviour
    {

        public enum MovementMode
        {
            Loop,
            PingPong
        }

        public static class PingPongIndexer
        {
            public static int GetNextIndex( int currentIndex, ref int moveDir, in int length)
            {
                var nextIndex = currentIndex + moveDir;

                if (nextIndex == length - 1) moveDir = -1;
                else if (nextIndex == 0) moveDir = 1;

                return nextIndex;
            }
        }

        public static class LoopIndexer
        {
            public static int GetNextIndex( in int currentIndex, in int length)
            {
                return (currentIndex + 1) % length;
            }
        }

        [SerializeField] private Vector3[] _points;
        [SerializeField] private Transform _movableTransform;
        [SerializeField] private MovementMode _movementMode = MovementMode.Loop;
        [SerializeField] private Countdown _countdown;

        private int _currentPointIndex = 0;
        private int _moveDirection = 1;

        private void Awake()
        {
            _movableTransform.localPosition = _points[_currentPointIndex];
        }

        private void OnEnable()
        {
            _countdown.OnExpired += MoveToNext;
        }

        private void OnDisable()
        {
            if(_countdown == null) return;
            _countdown.OnExpired -= MoveToNext;
        }

        private void MoveToNext()
        {
            _currentPointIndex = GetNextIndex(_currentPointIndex, ref _moveDirection);

            _movableTransform.localPosition = _points[_currentPointIndex];
            _countdown.Restart();
        }

        private int GetNextIndex(int currentIndex, ref int moveDirection)
        {
            return _movementMode == MovementMode.Loop
                ? LoopIndexer.GetNextIndex(currentIndex, _points.Length)
                : PingPongIndexer.GetNextIndex(currentIndex, ref moveDirection, _points.Length);
        }

        private void OnDrawGizmosSelected()
        {
            if(_points == null || _points.Length == 0) return;

            Transform parent = _movableTransform.parent;

            for (int i = 0; i < _points.Length; i++)
            {
                Gizmos.color = i == _currentPointIndex ? Color.green : Color.blue;

                var worldPosition = parent.TransformPoint( _points[i] );
                Gizmos.DrawSphere(worldPosition, 0.1f);
            }

            int previewDirection = _moveDirection;
            int previewIndex = GetNextIndex(_currentPointIndex, ref previewDirection);

            var currentPoint = parent.TransformPoint(_points[_currentPointIndex]);
            var nextPoint = parent.TransformPoint(_points[previewIndex]);

            var direction = nextPoint - currentPoint;
            var rotation = Quaternion.LookRotation(direction);

            Handles.ArrowHandleCap(0, currentPoint, rotation, 1f, EventType.Repaint);
        }
    }
}

using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace CountDown.Game
{
    public class LoopMotor : MonoBehaviour
    {

        public enum LoopMotorType
        {
            Loop,
            PingPong
        }

        public struct PingPongProvider
        {
            int _moveDir;

            public PingPongProvider(int moveDir)
            {
                _moveDir = (int)Mathf.Sign(moveDir);
            }


            public int GetNextIndex( int currentIndex, in int length)
            {
                var nextIndex = currentIndex + _moveDir;

                if (nextIndex == length - 1) _moveDir = -1;
                else if (nextIndex == 0) _moveDir = 1;

                return nextIndex;
            }
        }

        public struct LoopProvider
        {
            public readonly int GetNextIndex( in int currentIndex, in int length)
            {
                return (currentIndex + 1) % length;
            }
        }

        [SerializeField] private Vector3[] _points;
        [SerializeField] private Transform _loopedTransform;
        [SerializeField] private LoopMotorType _loopMotorType = LoopMotorType.Loop;
        [SerializeField] private Counter _counter;
        [SerializeField] private Parenter _parenter;

        private int _currentPointIndex = 0;
        private PingPongProvider _pingPongProvider = new (1);
        private readonly LoopProvider _loopProvider = new ();

        private void Awake()
        {
            _loopedTransform.localPosition = _points[_currentPointIndex];
        }

        private void OnEnable()
        {
            _counter.OnCounterEnded += MoveToNextPoint;
        }

        private void OnDisable()
        {
            if(_counter == null) return;
            _counter.OnCounterEnded -= MoveToNextPoint;
        }

        private void MoveToNextPoint()
        {
            _currentPointIndex = _loopMotorType == LoopMotorType.Loop ? 
                _loopProvider.GetNextIndex(_currentPointIndex, _points.Length) : 
                _pingPongProvider.GetNextIndex(_currentPointIndex, _points.Length);

            _loopedTransform.localPosition = _points[_currentPointIndex];
            _counter.ResetCounter();
        }

        private void OnDrawGizmosSelected()
        {
            if(_points == null || _points.Length == 0) return;
            for (int i = 0; i < _points.Length; i++)
            {
                Gizmos.color = i == _currentPointIndex ? Color.green : Color.blue;

                var worldTrtansform = transform.TransformPoint( _points[i] );
                Gizmos.DrawSphere(worldTrtansform, 0.1f);
            }

        }
    }
}

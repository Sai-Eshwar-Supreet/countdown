using UnityEngine;
using CountDown.Input;
using CountDown.Core;

namespace CountDown.Game
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private Vector2 _moveOffset = Vector2.one;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _obstacleMask;


        private readonly PlayerInputHandler _input = new();
        private float _spaceClearance;
        private const float GroundCheckDistance = 1.1f;

        protected override void Awake()
        {
            base.Awake();

            _spaceClearance = Mathf.Max(_moveOffset.x, _moveOffset.y) + 0.1f;

            _input.Initialize();
        }

        private void OnEnable()
        {
            _input.LockCursor();

            _input.Enable();

            _input.OnMove += TryMove;
        }

        private void OnDisable()
        {
            _input.OnMove -= TryMove;

            _input.Disable();
            _input.UnlockCursor();
        }

        private void TryMove(Vector2 moveInput)
        {
            var worldOffset = GetWorldMove(moveInput);
            var targetPosition = transform.position + worldOffset;

            if (!HasGround(targetPosition)) return;
            if (!IsSpaceClear(targetPosition)) return;

            transform.position = targetPosition;
            TurnManager.Instance.PassTurn();
        }

        public void Teleport(Vector3 destination)
        {
            transform.position = destination;
        }


        private Vector3 GetWorldMove(Vector2 moveInput)
        {
            var forward = transform.forward;
            var right = transform.right;
            forward.y = right.y = 0;

            forward.Normalize();
            right.Normalize();

             return _moveOffset.x * moveInput.x * right + 
                    _moveOffset.y * moveInput.y * forward;
        }

        private bool HasGround(Vector3 requestedLocation)
        {
            return Physics.Raycast(
                requestedLocation, 
                Vector3.down, 
                GroundCheckDistance, 
                _groundMask, 
                QueryTriggerInteraction.Ignore);
        }

        private bool IsSpaceClear(Vector3 requestedLocation)
        {
            Vector3 direction = (requestedLocation - transform.position).normalized;

            return !Physics.Raycast(
                transform.position, 
                direction, 
                _spaceClearance, 
                _obstacleMask, 
                QueryTriggerInteraction.Ignore);
        }
    }
}
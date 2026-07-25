using CountDown.Core;
using CountDown.Input;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CountDown.Game
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private float _moveDuration = 0.25f;
        [SerializeField] private Vector2 _moveOffset = Vector2.one;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _obstacleMask;


        private readonly PlayerInputHandler _input = new();
        private float _spaceClearance;
        private const float GroundCheckDistance = 1.1f;

        private bool _isMoving = false;
        private WaitForSeconds _moveDelay;


        protected override void Awake()
        {
            base.Awake();

            _moveDelay = new(_moveDuration);

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
            if (_isMoving) return;

            var worldOffset = GetWorldMove(moveInput);
            var targetPosition = transform.position + worldOffset;

            if (!HasGround(targetPosition, out Ground ground)) return;
            if (!IsSpaceClear(targetPosition)) return;

            StartCoroutine(MoveCoroutine(targetPosition, ground.MovementCost));
        }

        private IEnumerator MoveCoroutine(Vector3 targetPosition, int movementCost)
        {
            _isMoving = true;

            transform.position = targetPosition;

            yield return _moveDelay;

            TurnManager.Instance.PassTurn(movementCost);
            _isMoving = false;
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

        private bool HasGround(Vector3 requestedLocation, out Ground ground)
        {
            ground = null;

            var hasGround = Physics.Raycast(
                requestedLocation, 
                Vector3.down,
                out var hitInfo,
                GroundCheckDistance, 
                _groundMask, 
                QueryTriggerInteraction.Ignore);

            return hasGround && hitInfo.transform.TryGetComponent(out ground);
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
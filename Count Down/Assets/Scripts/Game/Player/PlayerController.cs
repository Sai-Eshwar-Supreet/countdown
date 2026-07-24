using DG.Tweening;
using CountDown.Input;
using UnityEngine;
using CountDown.Core;

namespace CountDown.Game
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private Vector2 _moveOffset = Vector2.one;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _obstacleMask;


        private readonly PlayerInputManager _playerInputManager = new();

        protected override void Awake()
        {
            base.Awake();

            _playerInputManager.Init();
        }

        private void OnEnable()
        {
            _playerInputManager.SetCursorState(true);

            _playerInputManager.Enable();

            _playerInputManager.OnMove += Move;
        }

        private void OnDisable()
        {
            _playerInputManager.OnMove -= Move;

            _playerInputManager.Disable();
            _playerInputManager.SetCursorState(false);
        }

        private void Move(Vector2 vector)
        {
            var worldMove = GetMove(vector);
            var worldPos = transform.position + worldMove;

            if (CheckGroundAvailability(worldPos) && CheckSpaceAvailability(worldPos))
            {
                transform.position = worldPos;

                TurnManager.Instance.PassTurn();
            }
        }

        public void Teleport(Vector3 targetPos)
        {
            transform.position = targetPos;
        }


        private Vector3 GetMove(Vector2 moveInput)
        {
            var forward = transform.forward;
            var right = transform.right;
            forward.y = right.y = 0;

            forward.Normalize();
            right.Normalize();

            var move = _moveOffset.x * moveInput.x * right + _moveOffset.y * moveInput.y * forward;

            return move;
        }

        private bool CheckGroundAvailability(Vector3 requestedLocation, float maxDistance = 1.1f)
        {
            Vector3 direction = Vector3.down;
            var ray = new Ray(requestedLocation, direction);

            bool isGroundAvailable = Physics.Raycast(ray, maxDistance, _groundMask, QueryTriggerInteraction.Ignore);
            return isGroundAvailable;
        }
        private bool CheckSpaceAvailability(Vector3 requestedLocation)
        {
            float maxDistance = Mathf.Max(_moveOffset.x, _moveOffset.y) + 0.1f;
            Vector3 direction = (requestedLocation - transform.position).normalized;

            var ray = new Ray(transform.position, direction);
            bool isSpaceAvailable = !Physics.Raycast(ray, maxDistance, _obstacleMask, QueryTriggerInteraction.Ignore);
            return isSpaceAvailable;
        }
    }
}
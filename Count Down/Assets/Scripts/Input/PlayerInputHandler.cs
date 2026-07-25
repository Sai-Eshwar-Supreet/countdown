using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CountDown.Input
{
    public class PlayerInputHandler
    {
        private PlayerInput.PlayerActions _playerActions;

        public event Action<Vector2> OnMove;

        public void Initialize()
        {
            _playerActions = new PlayerInput().Player;
        }

        public void LockCursor() => CursorUtility.Lock();
        public void UnlockCursor() => CursorUtility.Unlock();

        public void Enable()
        {
            _playerActions.Enable();

            _playerActions.Move.performed += HandleMove;
        }

        public void Disable()
        {
            _playerActions.Move.performed -= HandleMove;

            _playerActions.Disable();
        }

        private void HandleMove(InputAction.CallbackContext context)
        {
            var move = context.ReadValue<Vector2>();

            OnMove?.Invoke(move);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CountDown.Input
{
    public class PlayerInputManager
    {
        private PlayerInput.PlayerActions _playerActions;

        public event Action<Vector2> OnMove;

        public void Init()
        {
            _playerActions = new PlayerInput().Player;
        }

        public void SetCursorState(bool isLocked)
        {
            if (isLocked) CursorHandler.Lock();
            else CursorHandler.Unlock();
        }

        public void Enable()
        {
            _playerActions.Enable();

            _playerActions.Move.performed += MoveEventHandler;
        }

        public void Disable()
        {
            _playerActions.Move.performed -= MoveEventHandler;

            _playerActions.Disable();
        }

        private void MoveEventHandler(InputAction.CallbackContext context)
        {
            var move = context.ReadValue<Vector2>();
            if (move.x != 0 && move.y != 0) move.x = 0;

            OnMove?.Invoke(move);
        }
    }
}

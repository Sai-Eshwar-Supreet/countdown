using System;
using UnityEngine.InputSystem;

namespace CountDown.Input
{
    public class UIInputHandler
    {
        public event Action OnEscapePressed;
        public event Action OnLevelSelectPressed;
        public event Action OnRestartPressed;

        private PlayerUIInput.PlayerUIActions _uiActions;

        public void Initialize()
        {
            _uiActions = new PlayerUIInput().PlayerUI;
        }

        public void Enable()
        {
            _uiActions.Enable();
            _uiActions.Escape.performed += HandleEscapePress;
            _uiActions.LevelSelect.performed += HandleLevelSelectPress;
            _uiActions.Restart.performed += HandleRestartPress;
        }

        public void Disable()
        {
            _uiActions.Disable();
            _uiActions.Escape.performed -= HandleEscapePress;
            _uiActions.LevelSelect.performed -= HandleLevelSelectPress;
            _uiActions.Restart.performed -= HandleRestartPress;
        }

        private void HandleRestartPress(InputAction.CallbackContext context)
        {
            OnRestartPressed?.Invoke();
        }

        private void HandleEscapePress(InputAction.CallbackContext ctx)
        {
            OnEscapePressed?.Invoke();
        }

        private void HandleLevelSelectPress(InputAction.CallbackContext ctx)
        {
            OnLevelSelectPressed?.Invoke();
        }
    }
}

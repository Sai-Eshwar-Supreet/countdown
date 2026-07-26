using UnityEngine;

namespace CountDown.Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        public PlayerController PlayerController => _playerController;

        public bool IsPaused => _playerController == null || !_playerController.enabled;

        public bool GotKey { get; set; } = false;

        public void Pause(bool paused)
        {
            if (paused == IsPaused) return;
            _playerController.enabled = !paused;
        }
    }
}

using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelComplete : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HandleExit();
            }
        }

        private async void HandleExit()
        {
            await LevelManager.Instance.GoToNextLevel();
        }
    }
}

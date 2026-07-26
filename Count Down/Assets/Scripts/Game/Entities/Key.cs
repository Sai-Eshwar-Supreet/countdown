using CountDown.Core;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Key : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Level level = ServiceLocator.Get<Level>();
                if (level == null) return;

                level.GotKey = true;

                Destroy(gameObject);
            }
        }
    }
}

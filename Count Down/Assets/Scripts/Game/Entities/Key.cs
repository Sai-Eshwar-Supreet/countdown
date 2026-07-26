using CountDown.Core;
using CountDown.Sounds;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Key : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private SoundConfig _pickupConfig;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Level level = ServiceLocator.Get<Level>();
                if (level == null) return;

                level.GotKey = true;

                Destroy(gameObject);


                SoundManager.Play(_pickupConfig, gameObject.GetEntityId().ToString());
            }
        }
    }
}

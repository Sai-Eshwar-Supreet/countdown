using System;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(Collider))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform _teleportPoint;


        private void OnTriggerEnter(Collider other)
        {
            if(_teleportPoint == null) return;
            if(other.TryGetComponent(out PlayerController player))
            {
                player.Teleport(_teleportPoint.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            if (_teleportPoint != null)
            {
                Gizmos.DrawSphere(_teleportPoint.position, 0.1f);
            }
        }
    }
}

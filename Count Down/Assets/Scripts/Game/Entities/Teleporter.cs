using System;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform _destination;


        private void OnTriggerEnter(Collider other)
        {
            if(_destination == null) return;
            if (!other.TryGetComponent(out PlayerController player)) return;
            
            player.Teleport(_destination.position);
        }

        private void OnDrawGizmosSelected()
        {
            if (_destination == null) return;

            Gizmos.color = Color.cyan;
            
            Gizmos.DrawSphere(_destination.position, 0.1f);
            Gizmos.DrawLine(transform.position, _destination.position);
        }
    }
}

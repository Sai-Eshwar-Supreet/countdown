using DG.Tweening;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CountDown.Game
{
    public class MovableObject : MonoBehaviour
    {
        [SerializeField] private Counter _counter;

        [Header("MovableObject Settings")]
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private Collider _collider;
        [SerializeField] private Vector3 _unlockedPosition;
        [SerializeField] private Vector3 _lockedPosition;

        private void Awake()
        {
            _doorTransform.localPosition = _unlockedPosition;
            _collider.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            _counter.OnCounterEnded += OnCounterEnded;
        }

        private void OnDisable()
        {
            if (_counter == null) return;
            _counter.OnCounterEnded -= OnCounterEnded;
        }

        private void OnCounterEnded()
        {
            if (_doorTransform.localPosition == _lockedPosition) return;

            _collider.gameObject.SetActive(true);

            _doorTransform.localPosition = _lockedPosition;
        }

        private void OnDrawGizmosSelected()
        {
            if (_doorTransform == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + _unlockedPosition, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _lockedPosition, 0.1f);
        }
    }
}

using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class AttachmentZone : MonoBehaviour
    {
        private BoxCollider _collider;
        private Attachable _playerAttachable;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.isTrigger = true;
            _playerAttachable = PlayerController.Instance.GetComponent<Attachable>();
        }

        private void OnEnable()
        {
            TurnManager.Instance.OnPreTurn += UpdateAttachable;
            TurnManager.Instance.OnPostTurn += UpdateAttachable;
        }

        private void OnDisable()
        {
            if (TurnManager.Instance == null) return;
            TurnManager.Instance.OnPreTurn -= UpdateAttachable;
            TurnManager.Instance.OnPostTurn -= UpdateAttachable;
        }

        private void UpdateAttachable()
        {
            if(_playerAttachable == null) return;

            bool isPlayerInside = _collider.bounds.Contains(_playerAttachable.transform.position);
            if (isPlayerInside)
            {
                if (_playerAttachable.Parent != transform)  
                    _playerAttachable.AttachTo(transform);
            }
            else
            {
                if (_playerAttachable.Parent == transform) 
                    _playerAttachable.Detach();
            }
        }
    }
}

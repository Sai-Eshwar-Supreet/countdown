using CountDown.Core;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class AttachmentZone : MonoBehaviour
    {
        private BoxCollider _collider;
        private Attachable _playerAttachable;

        private Attachable PlayerAttachable
        {
            get
            {
                if(_playerAttachable == null)
                {
                    _playerAttachable = ServiceLocator.Get<Level>().PlayerController.GetComponent<Attachable>();
                }

                return _playerAttachable;
            }
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
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
            if(PlayerAttachable == null) return;

            bool isPlayerInside = _collider.bounds.Contains(PlayerAttachable.transform.position);
            if (isPlayerInside)
            {
                if (PlayerAttachable.Parent != transform)  
                    PlayerAttachable.AttachTo(transform);
            }
            else
            {
                if (PlayerAttachable.Parent == transform) 
                    PlayerAttachable.Detach();
            }
        }
    }
}

using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Parenter : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private ChildableObject _playerChildable;

        private void Awake()
        {
            _playerChildable = PlayerController.Instance.GetComponent<ChildableObject>();
        }

        private void OnEnable()
        {
            TurnManager.Instance.OnUpdateWorld += UpdateChildable;
        }

        private void OnDisable()
        {
            if (TurnManager.Instance == null) return;
            TurnManager.Instance.OnUpdateWorld -= UpdateChildable;
        }

        private void UpdateChildable()
        {
            if(_playerChildable == null) return;
            bool containsPosition = _collider.bounds.Contains(_playerChildable.transform.position);
            if (containsPosition)
            {
                if (_playerChildable.CurrentParent == transform) return;
                _playerChildable.SetParent(transform);
            }
            else
            {
                if (_playerChildable.CurrentParent != transform) return; 
                _playerChildable.ReturnToDefault();
            }
        }
    }
}

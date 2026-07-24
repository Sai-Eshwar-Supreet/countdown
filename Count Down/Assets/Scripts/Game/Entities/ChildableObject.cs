using UnityEngine;

namespace CountDown.Game
{
    public class ChildableObject : MonoBehaviour
    {
        private Transform _defaultParent;

        public Transform CurrentParent => transform.parent;

        public void Awake()
        {
            _defaultParent = transform.parent;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, true);
        }
        public void ReturnToDefault()
        {
            transform.SetParent(_defaultParent, true);
        }
    }
}

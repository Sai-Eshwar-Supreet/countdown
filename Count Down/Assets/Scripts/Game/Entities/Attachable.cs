using UnityEngine;

namespace CountDown.Game
{
    public class Attachable : MonoBehaviour
    {
        private Transform _defaultParent;

        public Transform Parent => transform.parent;

        private void Awake()
        {
            _defaultParent = transform.parent;
        }

        public void AttachTo(Transform parent)
        {
            transform.SetParent(parent, true);
        }
        public void Detach()
        {
            transform.SetParent(_defaultParent, true);
        }
    }
}

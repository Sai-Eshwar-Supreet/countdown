using TMPro;
using UnityEngine;

namespace CountDown.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Ground : MonoBehaviour
    {
        [SerializeField] private int _movementCost = 1;
        [SerializeField] private TextMeshPro _movementCostText;

        public int MovementCost => _movementCost;

        private void Awake()
        {
            if (_movementCostText == null) return;

            _movementCostText.SetText($"-{_movementCost}");
        }
    }
}

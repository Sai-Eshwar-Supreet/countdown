using System.Collections.Generic;
using UnityEngine;

namespace CountDown.Game
{
    [CreateAssetMenu(fileName = "LevelDataRegistry", menuName = "Levels/DataRegistry")]
    public class LevelDataRegistry : ScriptableObject
    {
        [SerializeField] private LevelData[] _levelDataList;

        public IReadOnlyList<LevelData> LevelDataList => _levelDataList;
    }
}

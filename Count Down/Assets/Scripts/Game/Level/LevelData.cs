using System.Collections.Generic;
using UnityEngine;

namespace CountDown.Game
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private LevelData _nextLevel;
        [SerializeField] private Level _levelPrefab;
        [SerializeField] private Sprite _sprite;

        public int ID => _id;
        public int NextLevelId => _nextLevel.ID;
        public Level LevelPrefab => _levelPrefab;
        public Sprite Sprite => _sprite;
    }
}

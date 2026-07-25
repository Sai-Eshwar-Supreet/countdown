using System;
using System.Collections.Generic;
using UnityEngine;

namespace CountDown.Game
{
    class GameDataManager : MonoBehaviour
    {
        [SerializeField] private LevelDataRegistry _levelDataRegistry;

        private readonly Dictionary<int, LevelData> _levelCache = new();

        public IReadOnlyList<LevelData> LevelDataList => _levelDataRegistry.LevelDataList;

        public void Load()
        {
            _levelCache.Clear();

            foreach (var levelData in _levelDataRegistry.LevelDataList)
            {
                if (!_levelCache.TryAdd(levelData.ID, levelData))
                {
                    Debug.LogError($"Duplicate level ID: {levelData.ID}");
                }
            }
        }

        public LevelData GetLevelData(int levelId)
        {
            if (_levelCache.TryGetValue(levelId, out var data))
            {
                return data;
            }

            throw new ArgumentException($"Invalid level id {levelId}", nameof(levelId));
        }

        public int GetNextLevelId(int currentLevelId)
        {
            if (_levelCache.TryGetValue(currentLevelId, out var data))
            {
                return data.NextLevelId;
            }

            throw new ArgumentException($"Invalid level id {currentLevelId}", nameof(currentLevelId));
        }
    }
}

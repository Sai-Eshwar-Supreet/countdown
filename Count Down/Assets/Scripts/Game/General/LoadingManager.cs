using System;
using UnityEngine;

namespace CountDown.Game
{
    public enum SceneType
    {
        Menu = 0,
        Level = 1,
    }

    public class LoadingManger : MonoBehaviour
    {
        [SerializeField] private SceneLoader _loader;

        public event Action<SceneType> OnLoaded;

        public void Load(SceneType type)
        {
            _loader.LoadScene((int)type, () => OnLoaded?.Invoke(type));
        }
    }
}

using CountDown.Core;
using CountDown.Input;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace CountDown.Game
{
    [DefaultExecutionOrder(-50)]
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private Button _exitToMenuButton;
        [SerializeField] private PauseUI _pauseUI;
        [SerializeField] private LevelSelectionUI _levelSelectionUI;

        private UIInputHandler _input;
        private int _currentLevel = -1;
        private Level _levelObject;

        protected override void Awake()
        {
            base.Awake();

            _input.Initialize();
        }

        private void OnEnable()
        {
            _levelSelectionUI.OnLevelPlayRequested += LoadLevel;
            _levelSelectionUI.OnOpen += PauseLevel;
            _levelSelectionUI.OnClose += ResumeLevel;
            _pauseUI.OnOpen += PauseLevel;
            _pauseUI.OnClose += ResumeLevel;
            _exitToMenuButton.onClick.AddListener(ExitLevel);

            LoadLevel(0);
            _levelSelectionUI.Init(_currentLevel);


            _input.Enable();

            _input.OnEscapePressed += OnEscapePressed;
            _input.OnLevelSelectPressed += OnLevelSelectPressed;
        }

        private void OnDisable()
        {
            _input.OnEscapePressed -= OnEscapePressed;
            _input.OnLevelSelectPressed -= OnLevelSelectPressed;

            _input.Disable();

            _levelSelectionUI.OnLevelPlayRequested -= LoadLevel;
            _levelSelectionUI.OnOpen -= PauseLevel;
            _levelSelectionUI.OnClose -= ResumeLevel;
            _pauseUI.OnOpen -= PauseLevel;
            _pauseUI.OnClose -= ResumeLevel;
            _exitToMenuButton.onClick.RemoveListener(ExitLevel);
        }

        public void LoadLevel(int levelId)
        {
            if (_currentLevel == levelId) return;

            // switch on loading canvas
            UnloadLevel();

            //fake load the level

            var levelData = ServiceLocator.Get<GameDataManager>().GetLevelData(levelId);

            _currentLevel = levelId;
            _levelObject = Instantiate(levelData.LevelPrefab);
            ServiceLocator.Register(_levelObject);

            _levelSelectionUI.Close();
            _pauseUI.Close();

            _levelSelectionUI.SetCurrentLevel(_currentLevel);
        }

        public void UnloadLevel()
        {
            if (_levelObject != null)
            {
                ServiceLocator.Unregister<Level>();
                Destroy(_levelObject.gameObject);
                _levelObject = null;
            }
        }

        public async Task GoToNextLevel()
        {
            if (_levelObject != null) _levelObject.Pause(true);

            var gameDataManager = ServiceLocator.Get<GameDataManager>();

            var nextLevelId = gameDataManager.GetNextLevelId(_currentLevel);

            await Task.Delay(500); // delay

            // show level completion ui for x duration

            LoadLevel(nextLevelId);
        }

        public void PauseLevel()
        {
            if (!AreUIsOpen()) return;
            if (_levelObject != null) _levelObject.Pause(true);
        }

        public void ResumeLevel()
        {
            if (AreUIsOpen()) return;
            if (_levelObject != null) _levelObject.Pause(false);
        }

        public void ExitLevel()
        {
            UnloadLevel();
            ServiceLocator.Get<LoadingManger>().Load(SceneType.Menu);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                if (AreUIsOpen()) return;
                _pauseUI.Open();
            }
        }

        private bool AreUIsOpen()
        {
            return _levelSelectionUI.IsOpen || _pauseUI.IsOpen;
        }
        private void OnEscapePressed()
        {
            if (_levelSelectionUI.IsOpen)
            {
                _levelSelectionUI.Close();
                return;
            }

            if (_pauseUI.IsOpen) _pauseUI.Close();
            else _pauseUI.Open();
        }

        private void OnLevelSelectPressed()
        {
            if (_levelSelectionUI.IsOpen) _levelSelectionUI.Close();
            else _levelSelectionUI.Open();
        }

    }

}

using CountDown.Core;
using CountDown.Input;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;

namespace CountDown.Game
{
    [DefaultExecutionOrder(-50)]
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private Button _exitToMenuButton;
        [SerializeField] private PauseUI _pauseUI;
        [SerializeField] private LevelSelectionUI _levelSelectionUI;
        [SerializeField] private CanvasGroup _completionScreen;

        private readonly UIInputHandler _input = new();
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
            
            _input.Enable();
            _input.OnEscapePressed += OnEscapePressed;
            _input.OnLevelSelectPressed += OnLevelSelectPressed;
            _input.OnRestartPressed += RestartLevel;

            _levelSelectionUI.Init(_currentLevel);
            LoadLevel(0);
        }

        private void OnDisable()
        {
            _input.OnEscapePressed -= OnEscapePressed;
            _input.OnLevelSelectPressed -= OnLevelSelectPressed;
            _input.OnRestartPressed -= RestartLevel;

            _input.Disable();

            _levelSelectionUI.OnLevelPlayRequested -= LoadLevel;
            _levelSelectionUI.OnOpen -= PauseLevel;
            _levelSelectionUI.OnClose -= ResumeLevel;
            _pauseUI.OnOpen -= PauseLevel;
            _pauseUI.OnClose -= ResumeLevel;
            _exitToMenuButton.onClick.RemoveListener(ExitLevel);
        }

        private void RestartLevel()
        {
            LoadLevel(_currentLevel);
        }

        public void LoadLevel(int levelId)
        {
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

            _completionScreen.DOFade(1, 0.25f);

            await Task.Delay(500); // delay

            _completionScreen.DOFade(0, 0.25f);

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

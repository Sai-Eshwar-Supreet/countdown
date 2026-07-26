using UnityEngine;
using UnityEngine.SceneManagement;
using CountDown.Sounds;
using System.Collections;
using System;

namespace CountDown.Game
{
    class SceneLoader : MonoBehaviour
    {
        [SerializeField] private LoadingUI _loadingUI;


        [Header("Sounds")]
        [SerializeField] private SoundConfig _sceneLoadSound;

        private readonly WaitForSeconds _sceneLoadDelay = new(0.25f);

        private bool _isLoading = false;

        public void LoadScene(int buildIndex, Action onCompleted)
        {
            if (_isLoading) return;
            StartCoroutine(LoadSceneCoroutine(buildIndex, onCompleted));
        }

        public IEnumerator LoadSceneCoroutine(int buildIndex, Action onCompleted)
        {
            _isLoading = true;

            _loadingUI.SetActive(true);
            SoundManager.Play(_sceneLoadSound, "Scene load");

            AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                _loadingUI.SetTargetProgress(operation.progress / 0.9f);
                yield return null;
            }
            _loadingUI.SetTargetProgress(1);

            while (!_loadingUI.IsFinished) yield return null;

            yield return _sceneLoadDelay; // delay to show 100% complete


            operation.allowSceneActivation = true;

            while (!operation.isDone) yield return null;

            _loadingUI.SetActive(false);

            _isLoading = false;

            onCompleted?.Invoke();
        }
    }
}

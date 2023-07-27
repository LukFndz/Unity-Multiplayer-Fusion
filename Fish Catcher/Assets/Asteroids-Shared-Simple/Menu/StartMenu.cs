using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.SceneManagement;

namespace Asteroids.SharedSimple
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _networkRunnerPrefab = null;

        [SerializeField] private TMP_InputField _roomName = null;
        [SerializeField] private string _gameSceneName = null;

        private NetworkRunner _runnerInstance = null;

        public void StartSharedSession()
        {
            StartGame(GameMode.Shared, _roomName.text, _gameSceneName);
        }

        private async void StartGame(GameMode mode, string roomName, string sceneName)
        {
            _runnerInstance = FindObjectOfType<NetworkRunner>();
            if (_runnerInstance == null)
            {
                _runnerInstance = Instantiate(_networkRunnerPrefab);
            }

            _runnerInstance.ProvideInput = true;

            var sceneObject = _runnerInstance.GetComponent<NetworkSceneManagerDefault>();

            var startGameArgs = new StartGameArgs()
            {
                GameMode = mode,
                SessionName = roomName,
                SceneManager = sceneObject,
                PlayerCount = 2
            };

            await _runnerInstance.StartGame(startGameArgs);

            _runnerInstance.SetActiveScene(sceneName);
        }
    }
}
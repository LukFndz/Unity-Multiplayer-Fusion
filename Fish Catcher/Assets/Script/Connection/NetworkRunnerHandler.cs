using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]
public class NetworkRunnerHandler : MonoBehaviour
{
    private NetworkRunner _networkRunner;

    // Start is called before the first frame update
    void Start()
    {
        _networkRunner = GetComponent<NetworkRunner>();

        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.Shared, SceneManager.GetActiveScene().buildIndex);
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, SceneRef sceneRef)
    {
        var sceneObject = runner.GetComponent<NetworkSceneManagerDefault>();

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Scene = sceneRef,
            SessionName = "Normal Lobby",
            SceneManager = sceneObject,
            PlayerCount = 2
        });
    }
}

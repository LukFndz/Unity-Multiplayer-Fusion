using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnNetworkPlayer : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPlayer _player;
    CharacterInputHandler _inputHandler;

    [SerializeField] private Transform[] _spawnPoints;

    #region CALLBACKS
    public void OnConnectedToServer(NetworkRunner runner)
    {
        if(runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("[CUSTOM MESSAGE] On Connected Server - Spawn Player as Local");
            runner.Spawn(_player, _spawnPoints[runner.LocalPlayer.PlayerId].position, Quaternion.identity, runner.LocalPlayer);

            //var list = new List<PlayerRef>(runner.ActivePlayers).Count;
            //FindObjectOfType<CanvasPlayer>().SetGameCount(list);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!NetworkPlayer.Local) return;

        if (!_inputHandler)
        {
            _inputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        }
        else
        {
            input.Set(_inputHandler.GetNetworkInput());
        }
    }


    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
       
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private NetworkPlayer _player;

    [SerializeField] private Transform[] _spawnPoints;


    public override void Spawned()
    {
        Debug.Log("SE CONECTO");
        if (Runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("[CUSTOM MESSAGE] On Connected Server - Spawn Player as Local");
            var player = Runner.Spawn(_player, _spawnPoints[Runner.LocalPlayer.PlayerId].position, Quaternion.identity, Runner.LocalPlayer);
            int number = Convert.ToInt32(Runner.LocalPlayer.PlayerId) + 1;
            player.transform.name = "Player_" + number;
        }
    }

}

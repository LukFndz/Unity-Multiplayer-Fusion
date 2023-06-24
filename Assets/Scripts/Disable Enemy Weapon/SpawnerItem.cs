using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnerItem : NetworkBehaviour
{
    public ItemUpgradeTripleShoot prefabTriple;
    public ItemUpgradeWeapon prefab;

    public List<Transform> _spawnPoints;

    public void SpawnItems()
    {
        Runner.Spawn(prefab, _spawnPoints[0].position, null);
        Runner.Spawn(prefab, _spawnPoints[1].position, null);
        Runner.Spawn(prefab, _spawnPoints[2].position, null);
        Runner.Spawn(prefab, _spawnPoints[3].position, null);
        Runner.Spawn(prefabTriple, _spawnPoints[4].position, null);
        RPC_StartGame();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    void RPC_StartGame()
    {
        StartCoroutine(CO_StartGame());
        Debug.Log("ACA");
    }

    IEnumerator CO_StartGame()
    {
        yield return new WaitForSeconds(0);
        foreach (var item in FindObjectsOfType<CharacterMovementHandler>())
        {
            item.blockInput = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnerItem : NetworkBehaviour
{
    public ItemUpgradeWeapon prefab;
    public List<Transform> _spawnPoints;

    public void SpawnItems()
    {
        Runner.Spawn(prefab, _spawnPoints[0].position, null);
        Runner.Spawn(prefab, _spawnPoints[1].position, null);
        Runner.Spawn(prefab, _spawnPoints[2].position, null);
        Runner.Spawn(prefab, _spawnPoints[3].position, null);
        Runner.Spawn(prefab, _spawnPoints[4].position, null);
    }
}

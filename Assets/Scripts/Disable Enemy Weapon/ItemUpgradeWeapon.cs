using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class ItemUpgradeWeapon : NetworkBehaviour
{
    TickTimer _expireTickTimer = TickTimer.None;
    private void Start()
    {
        if (Object.HasStateAuthority)
        {
            _expireTickTimer = TickTimer.CreateFromSeconds(Runner, 60);
        }
    }
    void DespawnObject()
    {
        _expireTickTimer = TickTimer.None;

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Object && Object.HasStateAuthority)
        {
            if (other.TryGetComponent(out WeaponHandler enemy))
            {
                enemy.ChangeBulletSpeed(2);
            }

            DespawnObject();
        }
    }

}

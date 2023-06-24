using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour
{
    [SerializeField] NetworkRigidbody _myNetRgbd;
    GameObject _owner;

    [Networked(OnChanged = nameof(OnSpeedChange))]
    float Speed { get; set; }
    TickTimer _expireTickTimer = TickTimer.None;



    private void Start()
    {
        _myNetRgbd.Rigidbody.AddForce(transform.forward * Speed, ForceMode.Impulse);

        if (Object.HasStateAuthority)
        {
            _expireTickTimer = TickTimer.CreateFromSeconds(Runner, 2);
        }        
    }

    public void SetSpeedAndOwner(float speed, GameObject owner)
    {
        Speed = speed;
        _owner = owner;
    }

    static void OnSpeedChange(Changed<Bullet> changed)
    {
        Debug.Log("CambioVel");
    }


    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (_expireTickTimer.Expired(Runner))
            {
                DespawnObject();
            }
        }
    }

    void DespawnObject()
    {
        _expireTickTimer = TickTimer.None;

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Object && Object.HasStateAuthority && other.gameObject != _owner)
        {
            if (other.TryGetComponent(out LifeHandler enemy))
            {
                enemy.TakeDamage(25);
            }

            DespawnObject();
        }
    }
}

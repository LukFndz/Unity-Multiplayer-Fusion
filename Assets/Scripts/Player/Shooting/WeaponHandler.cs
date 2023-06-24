using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponHandler : NetworkBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _firingTransform;
    [SerializeField] ParticleSystem _shootParticle;
    public float BulletSpeed;

    [Networked(OnChanged = nameof(OnFiringChange))]
    bool IsFiring { get; set; }

    float _lastFireTime;

    LifeHandler _lifeHandler;

    private void Awake()
    {
        _lifeHandler = GetComponent<LifeHandler>();
    }

    public override void FixedUpdateNetwork()
    {
        if (_lifeHandler.IsDead) return;

        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.isFirePressed)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        if (Time.time - _lastFireTime < 0.15f) return;

        _lastFireTime = Time.time;

        StartCoroutine(COR_Fire());

        Runner.Spawn(_bulletPrefab, _firingTransform.position, transform.rotation).ChangeSpeed(BulletSpeed);

    }

    public void ChangeBulletSpeed(float speed)
    {
        BulletSpeed += speed;
    }

    IEnumerator COR_Fire()
    {
        IsFiring = true;

        yield return new WaitForSeconds(0.15f);

        IsFiring = false;
    }

    static void OnFiringChange(Changed<WeaponHandler> changed)
    {
        bool isFiringCurrent = changed.Behaviour.IsFiring;

        changed.LoadOld();

        bool isFiringOld = changed.Behaviour.IsFiring;

        if (isFiringCurrent && !isFiringOld)
        {
            changed.Behaviour.FireRemote();
        }
    }

    void FireRemote()
    {
        _shootParticle.Play();
    }
}

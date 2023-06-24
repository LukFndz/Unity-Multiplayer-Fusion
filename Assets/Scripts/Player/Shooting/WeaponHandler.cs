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
    public float BulletAmount = 1;


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

    public void ChangeBulletAmount(int amount)
    {
        BulletAmount = amount;
    }

    void Fire()
    {
        if (Time.time - _lastFireTime < 0.5f) return;

        _lastFireTime = Time.time;


        if (BulletAmount == 1)
        {
            StartCoroutine(COR_Fire());
            Runner.Spawn(_bulletPrefab, _firingTransform.position,transform.rotation).SetSpeedAndOwner(BulletSpeed, transform.root.gameObject);
        } else
        {
            StartCoroutine(COR_Triple());
        }


    }
    IEnumerator COR_Triple()
    {

        Runner.Spawn(_bulletPrefab, _firingTransform.position, transform.rotation).SetSpeedAndOwner(BulletSpeed, transform.root.gameObject);

        yield return new WaitForSeconds(0.15f);

        Runner.Spawn(_bulletPrefab, _firingTransform.position, transform.rotation).SetSpeedAndOwner(BulletSpeed, transform.root.gameObject);

        yield return new WaitForSeconds(0.15f);

        Runner.Spawn(_bulletPrefab, _firingTransform.position, transform.rotation).SetSpeedAndOwner(BulletSpeed, transform.root.gameObject);

    }

    public void ChangeBulletSpeed(float speed)
    {
        BulletSpeed += speed;
    }

    IEnumerator COR_Fire()
    {
        IsFiring = true;

        yield return new WaitForSeconds(0.5f);

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

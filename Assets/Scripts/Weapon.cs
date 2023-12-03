using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BurstFire // = 2

    }

    public FireModes FireMode;
    public float Spread = 0f;
    public int BurstFireAmount = 3;
    public float BurstFireInterval = 0.1f;
    public int ProjectileCount = 1;

    public GameObject Projectile;

    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;

    public Transform SpawnPos;
    public Cooldown ShootInterval;

    private float _timer = 0f;
    private bool _canShoot = true;
    private bool _fireReset = true;

    public Cooldown ReloadCooldown;
    public int MaxBulletCount = 12;
    public int CurrentBulletCount {get {return currentBulletCount; } }
    
    protected int currentBulletCount = 12;

    
    void Start()
    {
        currentBulletCount = MaxBulletCount;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();
    }

    private void UpdateReloadCooldown()
    {
        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished)
            return;

        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            currentBulletCount = MaxBulletCount;
        }
        
        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready;
    }
    
    private void UpdateShootCooldown()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;
        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }

    public void Shoot()
    {
        if (Projectile == null)
        {
            Debug.LogWarning("Missing Projectile prefab");
            return;
        }

        if (SpawnPos == null)
        {
            Debug.LogWarning("Missing SpawnPosition transform");
            return;
        }

        if (ReloadCooldown.IsOnCooldown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready)
            return;
        
        switch (FireMode)
        {
            case FireModes.Auto:
            {
                AutoFireShoot();
                break;
            }
            case FireModes.SingleFire:
            {
                SingleFireShoot();
                break;
            }
            case FireModes.BurstFire:
            {
                BurstFireShoot();
                break;
            }
        }
    }

    void AutoFireShoot()
    {
        if (!_canShoot)
            return;

        if(ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        ShootProjectile();
        
        currentBulletCount--;

        ShootInterval.StartCooldown();
        
        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCooldown)
        {        
            foreach (var feedback in ReloadFeedbacks)
            {
                GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
            }
            
            ReloadCooldown.StartCooldown();
        }
        
    }

    void SingleFireShoot()
    {
        if (!_canShoot)
            return;

        if (!_fireReset)
            return;
        
        ShootProjectile();
        
        currentBulletCount--;

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCooldown)
        {        
            foreach (var feedback in ReloadFeedbacks)
            {
                GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
            }
            
            ReloadCooldown.StartCooldown();
        }
        
        _fireReset = false;
    }

    void BurstFireShoot()
    {
        if (!_canShoot)
            return;
        
        if (_burstFiring)
            return;
        
        if (!_fireReset)
            return;
        
        if(ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;
        
        StartCoroutine(BurstFireCo());
        
    }

    void ShootProjectile()
    {
        for (int i = 0; i < ProjectileCount; i++)
        {
            float randomRot = Random.Range(-Spread, Spread);

            GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0,0,randomRot));
        }
        
        SpawnFeedbacks();

    }

    protected bool _burstFiring = false;
    protected float _lastShootRequestAt;

    IEnumerator BurstFireCo()
    {
        _burstFiring = true;
        _fireReset = false;
        
        if (Time.time - _lastShootRequestAt < BurstFireInterval)
        {
            yield break;
        }
        
        int remainingShots = BurstFireAmount;
        
        while (remainingShots > 0  )
        {
            
            ShootProjectile();
            
            currentBulletCount--;

            if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCooldown)
            {        
                foreach (var feedback in ReloadFeedbacks)
                {
                    GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
                }
            
                ReloadCooldown.StartCooldown();
                break;
            }
            
            _lastShootRequestAt = Time.time;

            remainingShots--;
            yield return WaitFor(BurstFireInterval);
        }
        
        _burstFiring = false;

        ShootInterval.StartCooldown();
    }

    IEnumerator WaitFor(float seconds)
    {
        for (float timer = 0f; timer < seconds; timer += Time.deltaTime)
        {
            yield return null;
        }
    }
    
    public void StopShoot()
    {
        if (FireMode == FireModes.Auto)
            return;
        
        _fireReset = true;
    }

    void SpawnFeedbacks()
    {
        foreach (var feedback in Feedbacks)
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }

}
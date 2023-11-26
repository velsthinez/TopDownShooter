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
    public GameObject Projectile;
    
    public GameObject[] Feedbacks;
    
    public Transform SpawnPos;
    public Cooldown AutofireShootInterval;
    
    private float _timer = 0f;
    private bool _canShoot = true;
    private bool _singleFireReset = true;

    // Update is called once per frame
    void Update()
    {

        if (AutofireShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        AutofireShootInterval.CurrentProgress = Cooldown.Progress.Ready;
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
        }
    }

    void AutoFireShoot()
    {
        if (!_canShoot)
            return;

        if(AutofireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;
        
        float randomRot = Random.Range(-Spread, Spread);

        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0,0,randomRot));
        AutofireShootInterval.StartCooldown();
        SpawnFeedbacks();

    }

    void SingleFireShoot()
    {
        if (!_singleFireReset)
            return;
        
        float randomRot = Random.Range(-Spread, Spread);

        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0,0,randomRot));
        SpawnFeedbacks();
        
        _singleFireReset = false;
    }

    public void StopShoot()
    {
        _singleFireReset = true;
    }

    void SpawnFeedbacks()
    {
        foreach (var feedback in Feedbacks)
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }

}
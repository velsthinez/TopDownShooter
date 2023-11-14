using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BurstFire // = 2

    }

    public FireModes FireMode;
    
    public GameObject Projectile;
    public Transform SpawnPos;
    public float Interval = 0.1f;
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
        
        if(AutofireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;
        
        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);
        
        AutofireShootInterval.StartCooldown();
        
    }

    void AutoFireShoot()
    {
        if (!_canShoot)
            return;

        GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);
    }

    void SingleFireShoot()
    {
        if (!_singleFireReset)
            return;
        
        GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);
        _singleFireReset = false;
    }

    public void StopShoot()
    {
        _singleFireReset = true;
    }
}
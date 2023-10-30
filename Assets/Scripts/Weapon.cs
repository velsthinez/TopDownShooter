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

    private float _timer = 0f;
    private bool _canShoot = true;
    private bool _singleFireReset = true;

    // Update is called once per frame
    void Update()
    {
        if (_timer < Interval)
        {
            _timer += Time.deltaTime;
            _canShoot = false;
            return;
        }

        _timer = 0f;
        _canShoot = true;
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
            case (FireModes.SingleFire):
            {
                SingleFireShoot();
                break;
            }

            case (FireModes.Auto):
            {
                AutoFireShoot();
                break;
            }
        }
      
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPos;
    public float Interval = 0.1f;
    
    private float _timer = 0f;
    private bool _canShoot = true;

    private void Update()
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

        if (!_canShoot)
            return;
        
        GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);

    }
    
}

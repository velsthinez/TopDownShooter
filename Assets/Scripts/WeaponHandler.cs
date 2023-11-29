using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Weapon CurrentWeapon;
    public Transform GunPosition;

    protected bool _tryShoot = false;

    private void Update()
    {
        HandleInput();
        HandleWeapon();
    }

    protected virtual void HandleInput()
    {
        
    }


    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null)
            return;
        
        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;
        
        if (_tryShoot)
            CurrentWeapon.Shoot();
        else
            CurrentWeapon.StopShoot();

        
    }

    public void EquipWeapon(GameObject equipWeapon)
    {
        if (equipWeapon == null)
            return;

        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon.gameObject);
        }

        GameObject _weaponGO = GameObject.Instantiate(equipWeapon, GunPosition);
        Weapon weapon = _weaponGO.GetComponent<Weapon>();

        if (weapon == null)
            return;
        
        CurrentWeapon = weapon;
        
    }
}

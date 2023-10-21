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
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            return;

        CurrentWeapon = weapon;
    }
}

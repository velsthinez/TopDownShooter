using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : PickUp
{
    public GameObject Weapon;

    protected override void PickedUp(Collider2D col)
    {
        WeaponHandler weaponHandler = col.GetComponent<WeaponHandler>();

        if (weaponHandler == null)
            return;
        
        weaponHandler.EquipWeapon(Weapon);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHandler
{
    public Transform AimOffset;

    protected override void HandleInput()
    {
        if (Input.GetButton("Fire1"))
            _tryShoot = true;

        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;
    }

    public Vector2 AimPosition ()
    {
        if(CurrentWeapon != null)
            return new Vector2(AimOffset.position.x, AimOffset.position.y);
        
        return new Vector2(transform.position.x, transform.position.y);
    }

}
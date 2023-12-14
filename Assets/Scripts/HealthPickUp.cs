using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp
{
    public float HealAmount = 5f;
    
    protected override void PickedUp(Collider2D col)
    {
        Health health = col.GetComponent<Health>();

        if (health == null)
            return;
        
        health.Heal(HealAmount);

    }
}

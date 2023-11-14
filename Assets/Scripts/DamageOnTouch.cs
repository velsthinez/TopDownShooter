using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // think of it like radio broadcasting
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;

    public float Damage = 1f;
    public float PushForce = 10f;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If we hit something that doesn't belong in our TargetLayerMask
        if(!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        Debug.Log("hit target");
        Health targetHealth = col.gameObject.GetComponent<Health>();

        Debug.Log(targetHealth);
        
        if (targetHealth == null)
            return;

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }
        
        TryDamage(targetHealth);
    }

    private void TryDamage(Health targetHealth)
    {
        targetHealth.Damage(Damage, transform.gameObject);
        Debug.Log("hit " + targetHealth);
        OnHit?.Invoke();
    }
}

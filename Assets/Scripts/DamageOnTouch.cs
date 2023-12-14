using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageOnTouch : MonoBehaviour
{
    // think of it like radio broadcasting
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;

    public float MinDamage = 1f;
    public float MaxDamage = 1f;
    public float PushForce = 10f;
    
    public GameObject[] DamageableFeedbacks;
    public GameObject[] AnythingFeedbacks;
    
    public LayerMask TargetLayerMask;
    public LayerMask IgnoreLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (((IgnoreLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;
        
        if (((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            HitDamageable(col);
        else
            HitAnything(col);
    }
    
    private void HitAnything(Collider2D col)
    {
        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        OnHit?.Invoke();
        SpawnFeedbacks(AnythingFeedbacks);
    }

    private void HitDamageable(Collider2D col)
    {
        // // If we hit something that doesn't belong in our TargetLayerMask
        // if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
        //     return;

        Health targetHealth = col.gameObject.GetComponent<Health>();
        
        if (targetHealth == null)
            return;

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        TryDamage(targetHealth);
        SpawnFeedbacks(DamageableFeedbacks);
    }

    private void TryDamage(Health targetHealth)
    {
        float damageAmount = Random.Range(MinDamage, MaxDamage);
        targetHealth.Damage(damageAmount, transform.gameObject);
        OnHit?.Invoke();
    }
    
    void SpawnFeedbacks(GameObject[] Feedbacks)
    {
        foreach (var feedback in Feedbacks)
        {
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }
    }
}

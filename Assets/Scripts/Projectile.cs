using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public Cooldown Lifetime;
    
    private Rigidbody2D _rigidbody;
    private DamageOnTouch _damageOnTouch;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddRelativeForce(new Vector2(0f,Speed));

        _damageOnTouch = GetComponent<DamageOnTouch>();

        
        // subscribing
        if (_damageOnTouch != null) 
            _damageOnTouch.OnHit += Die;
        
        Lifetime.StartCooldown();
    }

    void Update()
    {
        if (Lifetime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    void Die()
    {
        // unsubscribing
        if (_damageOnTouch != null) 
            _damageOnTouch.OnHit -= Die;
        
        Lifetime.StopCooldown();
        Destroy(gameObject);
    }
    
}
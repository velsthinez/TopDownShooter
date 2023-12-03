using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;

    public delegate void DeathEvent();
    public DeathEvent OnDeath;
    
    public float MaxHealth = 10f;
    public Cooldown Invulnerable;
    
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    
    public float _currentHealth = 10f;
    private bool _canDamage = true;

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ResetInvulnerable();
    }

    private void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (Invulnerable.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
    }

    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }

        if (Invulnerable.Duration > 0)
        {
            Invulnerable.StartCooldown();
            _canDamage = false;

        }
        
        OnHit?.Invoke(source);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(this.gameObject);
    }
}

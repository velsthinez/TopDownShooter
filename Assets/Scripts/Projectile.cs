using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 10f;
    public float Speed = 10f;
    public float PushForce = 10f;
    public float Lifetime = 1f;
    public LayerMask TargetLayerMask;
    
    private Rigidbody2D _rigidbody;

    private float _timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddRelativeForce(new Vector2(0f,Speed));
    }

    void Update()
    {

        if (_timer < Lifetime)
        {
            _timer += Time.deltaTime;
            return;
        }

        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }
        
        Die();
    }
}
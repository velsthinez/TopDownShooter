using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MaxSpeed = 20f;
    public float Acceleration = 5f;
    public float Deceleration = -1000f;
    
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private bool _isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody == null || _collider == null)
            return;

        if (Input.GetKey(KeyCode.W) && _rigidbody.velocity.magnitude < MaxSpeed)
        {
            _rigidbody.AddForce(transform.up * (Acceleration * Time.deltaTime));
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        if ( !_isMoving && _rigidbody.velocity.magnitude > 0)   
            _rigidbody.AddForce(-_rigidbody.velocity * (Deceleration * Time.deltaTime));
    }
}

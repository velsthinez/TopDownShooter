using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 5f;
    protected float m_MovementSmoothing = .05f;

    protected Collider2D _collider;
    protected Rigidbody2D _rigidbody;

    protected bool _isMoving = false;

    protected Vector2 _inputDirection;
    protected Vector2 m_Velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
        HandleMovement();
    }

    protected virtual void HandleInput()
    {
    }
    
    private void HandleMovement()
    {
        if (_rigidbody == null || _collider == null)
            return;
        
        Vector2 targetVelocity = Vector2.zero;
        
        targetVelocity = new Vector2(_inputDirection.x * (Acceleration), _inputDirection.y * (Acceleration));

        _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);

        _isMoving = targetVelocity.x != 0 || targetVelocity.y != 0;
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (targetVelocity));
    }
}

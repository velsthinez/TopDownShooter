using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 5f;
    private float m_MovementSmoothing = .05f;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private bool _isMoving = false;

    private Vector2 _inputDirection;
    private Vector2 m_Velocity = Vector2.zero;

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

    private void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // _inputMovement.Normalize();

    }
    
    private void HandleMovement()
    {
        if (_rigidbody == null || _collider == null)
            return;
        
        Vector2 targetVelocity = Vector2.zero;
        
        targetVelocity = new Vector2(_inputDirection.x * (Acceleration), _inputDirection.y * (Acceleration));

        _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);

        _isMoving = targetVelocity.x != 0 || targetVelocity.y != 0;
    }
}

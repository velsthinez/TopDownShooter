using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Transform Target;
    public float RotationDregree = 22.5f;
    
    private Vector2 targetDirection;
    protected override void HandleInput()
    {
        if (Target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player == null)
                return;

            Target = player.transform;
        }
        
        GetIdealDirection();
        _inputDirection = (Target.position - transform.position).normalized;
    }

    void GetIdealDirection()
    {
        targetDirection = (Target.position - transform.position).normalized;
        
        Debug.DrawRay(transform.position , targetDirection,Color.green );

        for (int i = 0; i < 6; i++)
        {
            Debug.DrawRay(transform.position , Quaternion.Euler(0,0,RotationDregree*(i+1)) * targetDirection,Color.green );
            Debug.DrawRay(transform.position , Quaternion.Euler(0,0,-RotationDregree*(i+1)) * targetDirection,Color.green );
        }

    }
}

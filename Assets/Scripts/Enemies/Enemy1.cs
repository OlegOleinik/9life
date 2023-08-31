using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : AEnemy
{
    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        var dir = (enemiesController.TargetPoint - (Vector2)transform.position).normalized;
        rigidbody2D.position += dir * speed;
    }
}

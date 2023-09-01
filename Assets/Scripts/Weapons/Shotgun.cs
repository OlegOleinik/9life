using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunArgs
{
    public float scatterModifier = 1;
    public int bulletCountModifier = 1;
}

public class Shotgun : AWeaponBase
{
    [SerializeField] private int bulletCountBase = 5;
    [SerializeField] private float scatterBase = 10;

    private float nextShootTime = 0;
    private float scatter;
    private int bulletCount;

    public override void Init(BulletController bulletController)
    {
        base.Init(bulletController);
        nextShootTime = 0;
        scatter = scatterBase;
        bulletCount = bulletCountBase;
    }

    private void Update()
    {
        FollowMouse();
    }

    public override void Shoot(bool isDown = false)
    {
        if (isDown && Time.time > nextShootTime)
        {
            nextShootTime = Time.time + reloadTime;

            var mainDirection = GetDirection();
            
            int startCount = - (bulletCount / 2);
            int endCount = 0;
            if (bulletCount % 2 == 0)
            {
                mainDirection += (Vector2)(Quaternion.AngleAxis( scatter, Vector3.forward) * mainDirection);
                endCount = -startCount;
            }
            else
            {
                endCount = -(startCount) + 1;
            }

            for (int i = startCount; i < endCount; i++)
            {
                var direction = (Vector2)(Quaternion.AngleAxis( scatter * i, Vector3.forward) * mainDirection);
                BaseShoot(direction);
            }
        }
    }

    public override void SetArgs(object args)
    {
        var shotgunArgs = args as ShotgunArgs;
        if (shotgunArgs != null)
        {
            scatter = shotgunArgs.scatterModifier * scatterBase;
            bulletCount = shotgunArgs.bulletCountModifier * bulletCountBase;
        }
    }
}
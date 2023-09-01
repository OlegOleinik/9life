using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : AWeaponBase
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite bowReady;
    [SerializeField] private Sprite bowEmpty;

    private float nextShootTime = 0;
    private bool isPulled = false;
    private bool isReady = false;
    private void Update()
    {
        FollowMouse();
    }

    public override void Shoot(bool isDown = false)
    {
        if (isDown)
        {
            if (!isPulled)
            {
                isPulled = true;
                nextShootTime = Time.time + reloadTime;
                isReady = false;
            }
            else if(Time.time >= nextShootTime && !isReady)
            {
                isReady = true;
                spriteRenderer.sprite = bowReady;
            }
        }
        else if (isPulled)
        {
            if (Time.time >= nextShootTime)
            {
                var direction = GetDirection();
                BaseShoot(direction);
            }

            ResetBow();
        }
    }

    public override void SetInactive()
    {
        ResetBow();
        base.SetInactive();
    }

    private void ResetBow()
    {
        isPulled = false;
        isReady = false;
        spriteRenderer.sprite = bowEmpty;
    }
}

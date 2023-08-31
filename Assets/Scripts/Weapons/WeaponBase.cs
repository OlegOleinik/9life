using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponType
{
    Shotgun = 0,
}

public class WeaponBase: ScriptableObject
{
    [SerializeField] protected EWeaponType _type;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float reloadTime = 1;
    [SerializeField] protected Bullet _bulletPrefab;

    public EWeaponType Type => _type;
    public int Damage => _damage;
    
    protected BulletController bulletController;

    public virtual void Init(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }

    public Bullet Bullet => _bulletPrefab;

    public virtual void Shoot(bool isDown = false)
    {
        
    }

    public virtual void SetArgs(object args)
    {
        
    }
}

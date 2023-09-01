using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletController : AController
{
    [SerializeField] private Transform bulletsPoolTransform;
    
    private Action<bool> shoot = b => {};
    public Camera Camera => _camera;

    private Pool<Bullet> bulletsPool;
    private AWeaponBase weapon;
    private bool _isDown = false;
    private Camera _camera;
    
    private Player player;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Player, out player);
        _camera = Camera.main;
        bulletsPool = new Pool<Bullet>(() =>
        {
            var bullet = Instantiate(weapon.Bullet, bulletsPoolTransform);
            return bullet;
        }, Enum.GetValues(typeof(EWeaponType)).Cast<Enum>().ToList(), BulletInit);
    }

    private void Update()
    { 
        shoot.Invoke(_isDown);
    }

    public void SetIsDown(bool value) => _isDown = value;

    public void SetWeapon(AWeaponBase weapon)
    {
        if (this.weapon != null) 
            shoot -= this.weapon.Shoot;
        shoot += weapon.Shoot;
        this.weapon = weapon;
    }

    public Bullet GetBullet(EWeaponType type) => bulletsPool.GetFreeObject(type);

    private void OnBulletHide(Bullet bullet)
    {
        bulletsPool.AddFreeObject(bullet.WeaponType, bullet);
    }
    
    private void BulletInit(Bullet bullet)
    {
        bullet.Init(weapon.Type, OnBulletHide);
    }
}

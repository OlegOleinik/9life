using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : AController
{
    [SerializeField] private Transform bulletsPoolTransform;
    
    private Action<bool> shoot = b => {};
    public Camera Camera => _camera;
    
    private Dictionary<EWeaponType, LinkedList<Bullet>> freeBulletsPool =
        new Dictionary<EWeaponType, LinkedList<Bullet>>();
    private WeaponBase weapon;
    private bool _isDown = false;
    private Camera _camera;
    
    private Player player;

    private void Start()
    {
        Controllers.GetController(EControllerType.Player, out player);
        _camera = Camera.main;
        var weaponNames = (EWeaponType[])Enum.GetValues(typeof(EWeaponType));
        for (int i = 0; i < weaponNames.Length; i++)
        {
            freeBulletsPool.Add(weaponNames[i], new LinkedList<Bullet>());
        }
    }

    private void Update()
    { 
        shoot.Invoke(_isDown);
    }
    
    public void SetIsDown(bool value) => _isDown = value;

    public void SetWeapon(WeaponBase weapon)
    {
        if (this.weapon != null) 
            shoot -= this.weapon.Shoot;
        shoot += weapon.Shoot;
        this.weapon = weapon;
    }

    public Bullet GetBullet(EWeaponType weaponType)
    {
        if (freeBulletsPool[weaponType].Count < 1)
        {
            var bullet = Instantiate(weapon.Bullet, bulletsPoolTransform);
            bullet.Init(weaponType, OnBulletHide);
            return bullet;
        }
        else
        {
            var bullet = freeBulletsPool[weaponType].First.Value;
            freeBulletsPool[weaponType].RemoveFirst();
            return bullet;
        }
    }
    
    private void OnBulletHide(Bullet bullet)
    {
        freeBulletsPool[bullet.WeaponType].AddLast(bullet);
    }
}

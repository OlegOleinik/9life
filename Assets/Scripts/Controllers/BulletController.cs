using System;
using System.Linq;
using UnityEngine;

public class BulletController : AController
{
    [SerializeField] private Transform bulletsPoolTransform;
    
    public Camera Camera => camera;

    private Action<bool> shoot = b => {};
    private Pool<Bullet> bulletsPool;
    private AWeaponBase weapon;
    private bool isDown = false;
    private Camera camera;
    private Player player;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Player, out player);
        camera = Camera.main;
        bulletsPool = new Pool<Bullet>(() =>
        {
            var bullet = Instantiate(weapon.Bullet, bulletsPoolTransform);
            return bullet;
        }, Enum.GetValues(typeof(EWeaponType)).Cast<Enum>().ToList(), BulletInit);
    }

    private void Update()
    { 
        shoot.Invoke(isDown);
    }

    public void SetIsDown(bool value) => isDown = value;

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
        bulletsPool.TryAddFreeObject(bullet.WeaponType, bullet);
    }
    
    private void BulletInit(Bullet bullet)
    {
        bullet.Init(weapon.Type, OnBulletHide);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public enum EWeaponType
{
    Shotgun = 0,
    Bow = 1,
}

public abstract class AWeaponBase: MonoBehaviour
{
    [SerializeField] protected EWeaponType type;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float reloadTime = 1;
    [SerializeField] protected Bullet bulletPrefab;

    public EWeaponType Type => type;
    public int Damage => damage;
    
    protected BulletController bulletController;
    protected Player player;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Player, out player);
    }

    public virtual void Init(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }

    public Bullet Bullet => bulletPrefab;

    public virtual void Shoot(bool isDown = false)
    {
        
    }

    public virtual void SetArgs(object args)
    {
        
    }

    public virtual void SetActive()
    {
        gameObject.SetActive(true);
    }

    public virtual void SetInactive()
    {
        gameObject.SetActive(false);
    }

    protected virtual void FollowMouse() =>  player.WeaponTransform.right = GetDirection();

    protected virtual Vector2 GetDirection() => ((Vector2)(bulletController.Camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) -
                                                           player.BulletSpawnTransform.position)).normalized;

    protected void BaseShoot(Vector2 direction)
    {
        var bullet = bulletController.GetBullet(type);
        bullet.transform.position = player.BulletSpawnTransform.position;
        bullet.transform.right = direction;
        bullet.Fire(direction);
    }
}

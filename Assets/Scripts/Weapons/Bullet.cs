using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletModifier
{
    public float speedModifier = 1;
    public float hideDelayModifier = 1;
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private float baseFlySpeed = 1;
    [SerializeField] private float baseHideDelay = 3;
    [SerializeField] private Rigidbody2D rigidbody2D;

    private Action<Bullet> OnHide = bullet => { };
    public EWeaponType WeaponType {get; private set;}

    private float hideTime;
    private float speed;
    private bool isActive = false;
    private Vector2 direction;
    protected Player player;

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (Time.time >= hideTime)
            {
                Hide();
            }
            else
            {
                rigidbody2D.position += direction * speed;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hide();
        if (collider.CompareTag("Enemy"))
        {
            var damage = player.GetWeapon(WeaponType).Damage;
            var enemy = collider.GetComponent<AEnemy>();
            enemy.GetDamage(damage);
        }
    }
    
    public void Init(EWeaponType weaponType, Action<Bullet> OnHide)
    {
        this.WeaponType = weaponType;
        this.OnHide += OnHide;
        Controllers.Instance.GetController(EControllerType.Player, out player);
    }

    public void Fire(Vector2 direction, BulletModifier modifier = null)
    {
        isActive = true;
        gameObject.SetActive(true);
        this.direction = direction;
        if (modifier != null)
        {
            speed = modifier.speedModifier * baseFlySpeed;
            hideTime = Time.time + (modifier.hideDelayModifier * baseHideDelay);
        }
        else
        {
            speed = baseFlySpeed;
            hideTime = Time.time + baseHideDelay;
        }
    }

    private void Hide()
    {
        isActive = false;
        gameObject.SetActive(false);
        OnHide.Invoke(this);
    }
}
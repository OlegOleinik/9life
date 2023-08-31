using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AController, IMoveable, IDamagable
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private int baseHealth = 9;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private List<WeaponBase> weapons;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private TextMeshProUGUI healthText;
    
    public BoxCollider2D Collider2D => _collider;
    public Transform BulletSpawnTransform => _bulletSpawnTransform;
    
    private int currentWeaponIndex = 0;
    private int _health;

    private int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthText.text = value.ToString();
        }
    }
    private Vector2 direction;
    private BulletController bulletController;
        
    private void Start()
    {
        Controllers.GetController(EControllerType.Bullet, out bulletController);
        bulletController.SetWeapon(weapons[currentWeaponIndex]);

        foreach (var weapon in weapons)
        {
            weapon.Init(bulletController);
        }

        Health = baseHealth;
        healthText.text = Health.ToString();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnClickInput(InputAction.CallbackContext context)
    {
        bulletController.SetIsDown(context.ReadValue<float>() > 0.5f);
    }

    public WeaponBase GetWeapon(EWeaponType type)
    {
        if (weapons[currentWeaponIndex].Type == type)
            return weapons[currentWeaponIndex];

        foreach (var weapon in weapons)
        {
            if(weapon.Type == type)
                return weapon;
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void Move()
    {
        rigidbody2D.position += direction * speed;
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0) Die();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}

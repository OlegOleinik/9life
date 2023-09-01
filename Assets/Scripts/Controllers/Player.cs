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
    [SerializeField] private float weaponChangeDelay = 0.5f;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private List<AWeaponBase> weapons;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform weaponRootTransform;
    [SerializeField] private Transform _weaponTransform;
    
    public BoxCollider2D Collider2D => _collider;
    public Transform BulletSpawnTransform => _bulletSpawnTransform;
    public Transform WeaponTransform => _weaponTransform;
    
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
    
    private int currentWeaponIndex = 0;
    private int _health;
    private float nextWeaponChange = 0;
    private Vector2 direction;
    private BulletController bulletController;
    private WindowsController windowsController;
        
    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Bullet, out bulletController);
        Controllers.Instance.GetController(EControllerType.Windows, out windowsController);
        
        for (int i = 0; i < weapons.Count; i++)
        {
            var weapon = Instantiate(weapons[i], weaponRootTransform);
            weapons[i] = weapon;
            weapons[i].SetInactive();
        }
        SetWeapon(currentWeaponIndex);

        foreach (var weapon in weapons)
        {
            weapon.Init(bulletController);
        }

        Health = baseHealth;
        healthText.text = Health.ToString();
    }

    private void FixedUpdate()
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
    
    public void OnEscInput(InputAction.CallbackContext context)
    {
        windowsController.ShowWindow(EWindowType.Pause);
    }

    public void OnScrollWheelInput(InputAction.CallbackContext context)
    {
        if (Time.time < nextWeaponChange)
        {
            return;
        }
        nextWeaponChange= Time.time + weaponChangeDelay;
        var value = context.ReadValue<float>();
        var newIndex = 0;
        if (value > 0)
        {
            if (currentWeaponIndex >= weapons.Count - 1)
                newIndex = 0;
            else
                newIndex += currentWeaponIndex + 1;
        }
        else if (value < 0)
        {
            if (currentWeaponIndex <= 0)
                newIndex = weapons.Count - 1;
            else
                newIndex = currentWeaponIndex -1;
        }
        SetWeapon(newIndex);
    }

    public AWeaponBase GetWeapon(EWeaponType type)
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
        windowsController.ShowWindow(EWindowType.Fail);
    }

    private void SetWeapon(int index)
    {
        weapons[currentWeaponIndex].SetInactive();
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive();
        bulletController.SetWeapon(weapons[currentWeaponIndex]);
    }
}

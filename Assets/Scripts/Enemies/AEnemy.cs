using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public enum EEnemyType
{
    Enemy1 = 0,
    Enemy2 = 1,
}

public abstract class AEnemy : MonoBehaviour, IMoveable, IDamagable
{
    [SerializeField] protected EEnemyType _type;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected int baseHealth = 1;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected float damageDelay = 1;
    
    public EEnemyType Type => _type;
    
    protected EnemiesController enemiesController;
    protected Player player;
    protected int health;
    protected float nextDamage = 0;
    
    private Action<AEnemy> OnDie = enemy => { };

    protected virtual void Start()
    {
        Controllers.Instance.GetController(EControllerType.Enemies, out enemiesController);
        Controllers.Instance.GetController(EControllerType.Player, out player);
        health = baseHealth;
    }
    
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Time.time > nextDamage)
        {
            nextDamage = Time.time + damageDelay;
            player.GetDamage(damage);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemiesDespawnArea"))
        {
            Die();
        }
    }

    public void Init(Action<AEnemy> OnDie)
    {
        this.OnDie += OnDie;
    }
    
    public virtual void OnSpawnEnemy()
    {
        gameObject.SetActive(true);
    }

    public virtual void GetDamage(int damage)
    {
        health -= damage;
        if(health <= 0) Die();
    }
    
    public virtual void Die()
    {
        gameObject.SetActive(false);
        ResetEnemy();
        OnDie.Invoke(this);
    }

    public virtual void Move()
    {
        rigidbody2D.position += GetDirection() * speed;
    }
    protected virtual void ResetEnemy()
    {
        health = baseHealth;
    }

    protected virtual Vector2 GetDirection()
    {
        return (enemiesController.TargetPoint - (Vector2)transform.position).normalized;
    }
}

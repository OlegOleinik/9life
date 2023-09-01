using UnityEngine;

public class Enemy1 : AEnemy
{
    [SerializeField] private float damageScale = 1;

    private float speedModifier = 1;
    private Vector3 baseScale;

    protected override void Start()
    {
        base.Start();
        baseScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    public override void GetDamage(int damage)
    {
        transform.localScale *= damageScale;
        speedModifier *= 1 / damageScale;
        base.GetDamage(damage);
    }
    
    public override void Move()
    {
        rigidbody2D.position += GetDirection() * speed * speedModifier;
    }
    
    protected override void ResetEnemy()
    {
        base.ResetEnemy();
        transform.localScale = baseScale;
        speedModifier = 1;
    }
}

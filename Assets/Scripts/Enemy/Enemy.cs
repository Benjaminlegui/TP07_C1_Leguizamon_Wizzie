using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Spawn
{
    [Header("Shoot")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField, Min(1)] private int projectilePoolSize = 3;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Scriptable Objects")]
    [SerializeField] private EnemySO enemySO;

    public static event Action Died;

    private Health health;
    private float nextContactDamageTime;

    private EnemyContext context;
    private StateMachine<EnemyContext> stateMachine;

    private EnemyStateSetup states;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.Initialize(enemySO.MaxHealth);

        context = new EnemyContext
        {
            enemyTransform = transform,
            enemyBody = GetComponent<Rigidbody2D>(),
            enemyData = enemySO,
            firePoint = firePoint,
            projectilePool = new ObjectPool<Projectile>(projectilePrefab, projectilePoolSize),
            target = target != null ? target : FindTarget(),
            patrolOriginX = transform.position.x,
        };

        stateMachine = new StateMachine<EnemyContext>();

        states = new EnemyStateSetup(context, stateMachine);

        stateMachine.Initialize(states.Patrol);
    }

    private static Transform FindTarget()
    {
        Player player = FindFirstObjectByType<Player>();
        return player != null ? player.transform : null;
    }

    private void OnEnable() => health.Died += OnDied;
    private void OnDisable() => health.Died -= OnDied;

    private void OnDied()
    {
        SpawnDeathEffect();
        TryDropPickup();

        Died?.Invoke();
        Despawn();
    }

    private void SpawnDeathEffect()
    {
        if (enemySO.DeathEffect == null)
            return;

        Instantiate(enemySO.DeathEffect, transform.position, Quaternion.identity);
    }

    private void TryDropPickup()
    {
        if (enemySO.DropPrefab == null || UnityEngine.Random.value >= enemySO.DropChance)
            return;

        Instantiate(enemySO.DropPrefab, transform.position, Quaternion.identity);
    }

    public override void OnGet()
    {
        health.Initialize(enemySO.MaxHealth);
        context.patrolOriginX = transform.position.x;
        stateMachine.ChangeState(states.Patrol);
    }

    public void SetTarget(Transform newTarget)
    {
        context.target = newTarget;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryContactDamage(collision);
    private void OnCollisionStay2D(Collision2D collision) => TryContactDamage(collision);

    private void TryContactDamage(Collision2D collision)
    {
        if (Time.time < nextContactDamageTime)
            return;

        Player player = collision.collider.GetComponentInParent<Player>();

        if (player == null)
            return;

        player.GetComponent<Health>().TakeDamage(enemySO.ContactDamage);
        nextContactDamageTime = Time.time + enemySO.ContactDamageInterval;
    }
}

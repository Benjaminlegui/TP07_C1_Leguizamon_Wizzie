using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Spawn
{
    [Header("Shoot")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField, Min(1)] private int projectilePoolSize = 3;

    [Header("Scriptable Objects")]
    [SerializeField] private EnemySO enemySO;

    private Health health;

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
        };

        stateMachine = new StateMachine<EnemyContext>();

        states = new EnemyStateSetup(context, stateMachine);

        stateMachine.Initialize(states.Chase);
    }

    private void OnEnable() => health.Died += Despawn;
    private void OnDisable() => health.Died -= Despawn;

    public override void OnGet()
    {
        health.Initialize(enemySO.MaxHealth);
        stateMachine.ChangeState(states.Chase);
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
}

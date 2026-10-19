using UnityEngine;

public class EnemyShootState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private float shootTimer;

    public EnemyShootState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states)
        : base(context, stateMachine)
    {
        this.states = states;
    }

    protected override void OnEnter()
    {
        context.MoveHorizontal(0f);
        context.FaceTarget();

        Fire();
        shootTimer = context.enemyData.ShootInterval;
    }

    protected override void OnUpdate()
    {
        context.FaceTarget();

        shootTimer -= Time.deltaTime;

        if (shootTimer > 0f)
            return;

        if (!context.IsTargetInRange)
        {
            stateMachine.ChangeState(states.Chase);
            return;
        }

        Fire();
        shootTimer = context.enemyData.ShootInterval;
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(0f);
    }

    private void Fire()
    {
        Projectile projectile = context.projectilePool.Get(
            context.firePoint.position,
            Quaternion.identity
        );

        projectile.Launch(context.DirectionToTarget);
    }
}

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
        shootTimer = context.enemyData.ShootInterval;
        context.MoveHorizontal(0f);
    }

    protected override void OnUpdate()
    {
        if (!context.IsTargetInRange)
        {
            stateMachine.ChangeState(states.Chase);
            return;
        }

        context.FaceTarget();

        shootTimer -= Time.deltaTime;

        if (shootTimer > 0f)
            return;

        shootTimer = context.enemyData.ShootInterval;
        Fire();
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

using UnityEngine;

public class EnemyShootState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private readonly AnimationClip shootAnimation;
    private readonly AnimationClip idleAnimation;
    private float shootTimer;
    private float timeSinceShot;

    public EnemyShootState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states,
        AnimationClip shootAnimation,
        AnimationClip idleAnimation)
        : base(context, stateMachine)
    {
        this.states = states;
        this.shootAnimation = shootAnimation;
        this.idleAnimation = idleAnimation;
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

        timeSinceShot += Time.deltaTime;

        if (timeSinceShot >= shootAnimation.length)
            context.PlayAnimation(idleAnimation);

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

        timeSinceShot = 0f;
        context.PlayAnimation(shootAnimation, true);
    }
}

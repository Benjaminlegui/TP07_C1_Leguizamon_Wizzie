using UnityEngine;

public class EnemyIdleState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private readonly AnimationClip idleAnimation;
    private float waitTimer;

    public EnemyIdleState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states,
        AnimationClip idleAnimation)
        : base(context, stateMachine)
    {
        this.states = states;
        this.idleAnimation = idleAnimation;
    }

    protected override void OnEnter()
    {
        waitTimer = context.enemyData.PatrolWaitTime;
        context.MoveHorizontal(0f);
        context.PlayAnimation(idleAnimation);
    }

    protected override void OnUpdate()
    {
        if (context.IsTargetDetected)
        {
            stateMachine.ChangeState(states.Chase);
            return;
        }

        waitTimer -= Time.deltaTime;

        if (waitTimer > 0f)
            return;

        context.Face(-context.FacingDirection);
        stateMachine.ChangeState(states.Patrol);
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(0f);
    }
}

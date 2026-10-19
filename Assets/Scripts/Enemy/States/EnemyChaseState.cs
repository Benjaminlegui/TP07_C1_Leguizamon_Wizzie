using UnityEngine;

public class EnemyChaseState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private readonly AnimationClip walkAnimation;
    private readonly AnimationClip idleAnimation;

    public EnemyChaseState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states,
        AnimationClip walkAnimation,
        AnimationClip idleAnimation)
        : base(context, stateMachine)
    {
        this.states = states;
        this.walkAnimation = walkAnimation;
        this.idleAnimation = idleAnimation;
    }

    protected override void OnUpdate()
    {
        if (context.IsTargetLost)
        {
            stateMachine.ChangeState(states.Patrol);
            return;
        }

        if (context.IsTargetInRange)
        {
            stateMachine.ChangeState(states.Shoot);
            return;
        }

        context.FaceTarget();
    }

    protected override void OnFixedUpdate()
    {
        if (!context.HasTarget)
            return;

        if (!context.IsGroundAhead)
        {
            context.MoveHorizontal(0f);
            context.PlayAnimation(idleAnimation);
            return;
        }

        context.MoveHorizontal(context.DirectionToTarget);
        context.PlayAnimation(walkAnimation);
    }
}

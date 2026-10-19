using UnityEngine;

public class EnemyPatrolState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private readonly AnimationClip walkAnimation;
    private float direction = 1f;

    public EnemyPatrolState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states,
        AnimationClip walkAnimation)
        : base(context, stateMachine)
    {
        this.states = states;
        this.walkAnimation = walkAnimation;
    }

    protected override void OnEnter()
    {
        direction = context.FacingDirection;
        context.Face(direction);
        context.PlayAnimation(walkAnimation);
    }

    protected override void OnUpdate()
    {
        if (context.IsTargetDetected)
        {
            stateMachine.ChangeState(states.Chase);
            return;
        }

        float offset = context.enemyTransform.position.x - context.patrolOriginX;
        float patrolDistance = context.enemyData.PatrolDistance;

        bool reachedRight = direction > 0f && offset >= patrolDistance;
        bool reachedLeft = direction < 0f && offset <= -patrolDistance;

        if (!reachedRight && !reachedLeft && context.IsGroundAhead)
            return;

        stateMachine.ChangeState(states.Idle);
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(direction, context.enemyData.PatrolSpeed);
    }
}

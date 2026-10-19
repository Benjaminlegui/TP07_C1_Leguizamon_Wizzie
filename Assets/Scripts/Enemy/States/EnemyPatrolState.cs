public class EnemyPatrolState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;
    private float direction = 1f;

    public EnemyPatrolState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states)
        : base(context, stateMachine)
    {
        this.states = states;
    }

    protected override void OnEnter()
    {
        context.Face(direction);
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

        if (!reachedRight && !reachedLeft)
            return;

        direction = -direction;
        context.Face(direction);
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(direction, context.enemyData.PatrolSpeed);
    }
}

public class EnemyChaseState : State<EnemyContext>
{
    private readonly EnemyStateSetup states;

    public EnemyChaseState(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        EnemyStateSetup states)
        : base(context, stateMachine)
    {
        this.states = states;
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

        context.MoveHorizontal(context.DirectionToTarget);
    }
}

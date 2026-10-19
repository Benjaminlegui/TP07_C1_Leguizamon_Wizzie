public class EnemyStateSetup
{
    public EnemyPatrolState Patrol { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyShootState Shoot { get; private set; }

    public EnemyStateSetup(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine)
    {
        Patrol = new EnemyPatrolState(context, stateMachine, this);
        Chase = new EnemyChaseState(context, stateMachine, this);
        Shoot = new EnemyShootState(context, stateMachine, this);
    }
}

public class EnemyStateSetup
{
    public EnemyChaseState Chase { get; private set; }
    public EnemyShootState Shoot { get; private set; }

    public EnemyStateSetup(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine)
    {
        Chase = new EnemyChaseState(context, stateMachine, this);
        Shoot = new EnemyShootState(context, stateMachine, this);
    }
}

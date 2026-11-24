public class EnemyStateSetup
{
    public EnemyIdleState Idle { get; private set; }
    public EnemyPatrolState Patrol { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyShootState Shoot { get; private set; }

    public EnemyStateSetup(
        EnemyContext context,
        StateMachine<EnemyContext> stateMachine,
        // Suggestion: El enemigo usa PlayerAnimationsSO y sus clips Run y Throw. Renombrá el SO a algo genérico (CharacterAnimationsSO) para que el nombre no confunda.
        PlayerAnimationsSO animations)
    {
        Idle = new EnemyIdleState(context, stateMachine, this, animations.Idle.AnimationClip);

        Patrol = new EnemyPatrolState(context, stateMachine, this, animations.Run.AnimationClip);

        Chase = new EnemyChaseState(
            context,
            stateMachine,
            this,
            animations.Run.AnimationClip,
            animations.Idle.AnimationClip
        );

        Shoot = new EnemyShootState(
            context,
            stateMachine,
            this,
            animations.Throw.AnimationClip,
            animations.Idle.AnimationClip
        );
    }
}

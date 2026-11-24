using UnityEngine;

public class PlayerIdleState : State<PlayerContext>
{
    private readonly AnimationClip animation;

    public PlayerIdleState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        AnimationClip animation)
        : base(context, stateMachine)
    {
        this.animation = animation;
    }

    protected override void OnEnter()
    {
        // Warning: Animator.Play(animation.name) depende de que el estado del Animator se llame igual que el clip; si alguien renombra uno, falla en silencio.
        // Este patrón se repite en todos los estados del Player y en EnemyContext.PlayAnimation. Validá con animator.HasState o usá hashes de estado constantes.
        context.playerAnimator.Play(animation.name);
    }
}

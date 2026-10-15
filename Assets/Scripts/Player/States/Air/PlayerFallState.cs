using UnityEngine;

public class PlayerFallState : State<PlayerContext>
{
    private readonly AnimationClip animation;
    private static readonly int FallAnimSpeedHash = Animator.StringToHash("FallAnimSpeed");

    public PlayerFallState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        AnimationClip animation)
        : base(context, stateMachine)
    {
        this.animation = animation;
    }

    protected override void OnEnter()
    {
        context.playerAnimator.SetFloat(FallAnimSpeedHash, animation.length / context.RiseTime);
        context.playerAnimator.Play(animation.name);
    }
}

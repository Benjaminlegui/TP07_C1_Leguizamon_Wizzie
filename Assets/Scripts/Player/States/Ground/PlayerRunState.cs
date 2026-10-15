using UnityEngine;

public class PlayerRunState : State<PlayerContext>
{
    private readonly AnimationClip animation;

    public PlayerRunState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        AnimationClip animation)
        : base(context, stateMachine)
    {
        this.animation = animation;
    }

    protected override void OnEnter()
    {
        context.playerAnimator.Play(animation.name);
    }
}

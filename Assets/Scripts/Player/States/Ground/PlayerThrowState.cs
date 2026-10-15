using UnityEngine;

public class PlayerThrowState : State<PlayerContext>
{
    private readonly AnimationClip animation;

    public PlayerThrowState(
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

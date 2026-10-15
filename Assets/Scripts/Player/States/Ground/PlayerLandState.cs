using UnityEngine;

public class PlayerLandState : State<PlayerContext>
{
    private readonly AnimationClip animation;
    private float elapsedTime;

    public bool IsFinished => elapsedTime >= animation.length;

    public PlayerLandState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        AnimationClip animation)
        : base(context, stateMachine)
    {
        this.animation = animation;
    }

    protected override void OnEnter()
    {
        elapsedTime = 0f;
        context.playerAnimator.Play(animation.name);
    }

    protected override void OnUpdate()
    {
        elapsedTime += Time.deltaTime;
    }
}

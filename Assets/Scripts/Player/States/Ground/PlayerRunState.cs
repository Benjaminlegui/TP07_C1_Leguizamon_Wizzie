using UnityEngine;

public class PlayerRunState : State<PlayerContext>
{
    private readonly AnimationClip animation;
    private static readonly int RunAnimSpeedHash = Animator.StringToHash("RunAnimSpeed");

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
        UpdateAnimationSpeed();
        context.playerAnimator.Play(animation.name);
    }

    protected override void OnUpdate()
    {
        UpdateAnimationSpeed();
    }

    private void UpdateAnimationSpeed()
    {
        float currentSpeed = Mathf.Abs(context.moveInput * context.playerData.MoveSpeed);
        float animationSpeed = currentSpeed / context.playerData.RunClipReferenceSpeed;

        context.playerAnimator.SetFloat(RunAnimSpeedHash, animationSpeed);
    }
}

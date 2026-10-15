using UnityEngine;

public class PlayerJumpState : State<PlayerContext>
{
    private readonly AnimationClip animation;
    private static readonly int JumpAnimSpeedHash = Animator.StringToHash("JumpAnimSpeed");

    public PlayerJumpState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        AnimationClip animation)
        : base(context, stateMachine)
    {
        this.animation = animation;
    }

    protected override void OnEnter()
    {
        context.playerAnimator.SetFloat(JumpAnimSpeedHash, animation.length / context.RiseTime);
        context.playerAnimator.Play(animation.name);
        
        context.playerBody.linearVelocity = new Vector2(
            context.playerBody.linearVelocity.x,
            context.playerData.JumpSpeed
        );
    }
    
    protected override void OnUpdate()
    {}

    protected override void OnExit()
    {
        
    }
}

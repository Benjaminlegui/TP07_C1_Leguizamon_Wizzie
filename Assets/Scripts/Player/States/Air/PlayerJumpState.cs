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
        
        // Warning: Esta velocidad se setea desde OnEnter, que se llama en Update. Los cambios al Rigidbody2D van en FixedUpdate;
        // guardá un flag de salto pendiente y aplicalo en OnFixedUpdate.
        context.playerBody.linearVelocity = new Vector2(
            context.playerBody.linearVelocity.x,
            context.playerData.JumpSpeed
        );

        context.RaiseJumped();
    }
    
    // Suggestion: OnUpdate y OnExit vacíos no aportan nada, la clase base ya los define vacíos. Borralos.
    protected override void OnUpdate()
    {}

    protected override void OnExit()
    {
        
    }
}

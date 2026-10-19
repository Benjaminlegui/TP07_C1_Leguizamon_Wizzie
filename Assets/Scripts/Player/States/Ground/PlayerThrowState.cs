using UnityEngine;

public class PlayerThrowState : State<PlayerContext>
{
    private readonly AnimationClip animation;
    private static readonly int ThrowAnimSpeedHash = Animator.StringToHash("ThrowAnimSpeed");
    private float elapsedTime;
    private bool hasFired;
    private float animationSpeed = 1f;

    public bool IsFinished => elapsedTime >= animation.length / animationSpeed;

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
        elapsedTime = 0f;
        hasFired = false;
        animationSpeed = context.playerData.ThrowClipReleaseTime / context.playerData.ThrowFireTime;
        context.playerAnimator.SetFloat(ThrowAnimSpeedHash, animationSpeed);
        context.playerAnimator.Play(animation.name);
    }

    protected override void OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (!hasFired && elapsedTime >= context.playerData.ThrowFireTime)
        {
            hasFired = true;
            Fire();
        }
    }

    private void Fire()
    {
        Fireball fireball = context.fireballPool.Get(
            context.throwFirePoint.position,
            Quaternion.identity
        );

        fireball.Launch(context.facingDirection);
        context.RaiseThrew();
    }
}

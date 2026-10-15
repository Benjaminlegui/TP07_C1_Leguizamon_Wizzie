using UnityEngine;

public class PlayerStateSetup
{
    public PlayerGroundState Grounded { get; private set; }
    public PlayerAirState Airborne { get; private set; }
    public PlayerIdleState Idle { get; private set; }
    public PlayerJumpState Jump { get; private set; }
    public PlayerFallState Fall { get; private set; }
    public PlayerLandState Land { get; private set; }
    public PlayerRunState Run { get; private set; }
    public PlayerThrowState Throw { get; private set; }

    public PlayerStateSetup(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        PlayerAnimationsSO animations)
    {
        Grounded = new PlayerGroundState(context, stateMachine, this);
        
        Airborne = new PlayerAirState(context, stateMachine, this);
        
        Jump = new PlayerJumpState(context, stateMachine, animations.Jump.AnimationClip);
        
        Idle = new PlayerIdleState(
            context,
            stateMachine,
            animations.Idle.AnimationClip
        );
        
        Fall = new PlayerFallState(context, stateMachine, animations.Fall.AnimationClip);
        
        Land = new PlayerLandState(context, stateMachine, animations.Land.AnimationClip);

        Run = new PlayerRunState(context, stateMachine, animations.Run.AnimationClip);

        Throw = new PlayerThrowState(context, stateMachine, animations.Throw.AnimationClip);

        Grounded.SetInitialSubState(Idle);
        Airborne.SetInitialSubState(Fall);
    }
}

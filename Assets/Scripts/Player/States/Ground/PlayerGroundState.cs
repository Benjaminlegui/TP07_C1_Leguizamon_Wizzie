using UnityEngine;

public class PlayerGroundState : State<PlayerContext>
{
    private readonly PlayerStateSetup states;

    public PlayerGroundState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        PlayerStateSetup states)
        : base(context, stateMachine)
    {
        this.states = states;
    }
    
    protected override void OnUpdate()
    {
        if (!context.isGrounded)
        {
            stateMachine.ChangeState(states.Airborne);
            return;
        }

        if (Input.GetKeyDown(context.playerData.PlayerJump))
        {
            stateMachine.ChangeState(states.Airborne, states.Jump);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeSubState(states.Throw);
            return;
        }

        if (context.moveInput != 0f)
        {
            ChangeSubState(states.Run);
            return;
        }


        if (currentSubState == states.Land && states.Land.IsFinished)
        {
            ChangeSubState(states.Idle);
            return;
        }

        if (currentSubState == states.Run)
        {
            ChangeSubState(states.Idle);
        }
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(1f);
    }
}

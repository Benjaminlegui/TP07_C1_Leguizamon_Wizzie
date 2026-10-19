using UnityEngine;

public class PlayerAirState : State<PlayerContext>
{
    private readonly PlayerStateSetup states;
    public PlayerAirState(
        PlayerContext context,
        StateMachine<PlayerContext> stateMachine,
        PlayerStateSetup states)
        : base(context, stateMachine)
    {
        this.states = states;
    }

    protected override void OnUpdate()
    {
        float velocityY = context.playerBody.linearVelocity.y;
        float velocityX = context.playerBody.linearVelocity.x;
        
        if (context.isGrounded && velocityY <= 0f && velocityX <= 0f)
        {
            stateMachine.ChangeState(states.Grounded, states.Land);
            return;
        }

        if (context.isGrounded && velocityX != 0f && velocityY <= 0f)  
        {
            stateMachine.ChangeState(states.Grounded, states.Run);
            return;
        }
        
        if (currentSubState == states.Jump && velocityY <= 0f)
            ChangeSubState(states.Fall);
    }

    protected override void OnExit()
    {
        context.RaiseLanded();
    }

    protected override void OnFixedUpdate()
    {
        context.MoveHorizontal(context.playerData.AirControl);
    }
}

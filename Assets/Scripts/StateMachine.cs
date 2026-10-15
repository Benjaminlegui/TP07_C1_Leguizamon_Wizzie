public class StateMachine<TContext>
{
    public State<TContext> CurrentState { get; private set; }

    public void Initialize(State<TContext> startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
    
    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }

    public void ChangeState(State<TContext> newState, State<TContext> subState = null)
    {
        if (CurrentState == newState)
            return;
        
        CurrentState?.Exit();
        
        CurrentState = newState;
        
        CurrentState?.Enter(subState);
    }
}

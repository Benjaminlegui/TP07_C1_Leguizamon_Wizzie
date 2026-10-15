public abstract class State<TContext>
{
    protected readonly TContext context;
    protected readonly StateMachine<TContext> stateMachine;

    protected State<TContext> parentState;
    protected State<TContext> currentSubState;
    private State<TContext> initialSubState;

    public bool IsActive { get; private set; }

    protected State(TContext context, StateMachine<TContext> stateMachine)
    {
        this.context = context;
        this.stateMachine = stateMachine;
    }

    public void Enter(State<TContext> subState = null)
    {
        IsActive = true;
        OnEnter();

        currentSubState = subState ?? initialSubState;

        if (currentSubState == null)
            return;

        currentSubState.parentState = this;
        currentSubState.Enter();
    }

    public void Update()
    {
        OnUpdate();

        if (!IsActive)
            return;

        currentSubState?.Update();
    }

    public void FixedUpdate()
    {
        OnFixedUpdate();

        if (!IsActive)
            return;

        currentSubState?.FixedUpdate();
    }

    public void Exit()
    {
        currentSubState?.Exit();
        currentSubState = null;

        OnExit();
        IsActive = false;
    }

    protected virtual void OnEnter()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnExit()
    {
    }

    protected void ChangeSubState(State<TContext> newSubState)
    {
        if (currentSubState == newSubState)
            return;

        currentSubState?.Exit();
        currentSubState = newSubState;

        if (newSubState == null)
            return;

        newSubState.parentState = this;
        newSubState.Enter();
    }

    internal void SetInitialSubState(State<TContext> subState)
    {
        initialSubState = subState;
        subState.parentState = this;
    }
}

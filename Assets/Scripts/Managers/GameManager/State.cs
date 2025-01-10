public abstract class State<T> where T : IStateMachine
{
    protected T context;
    public int Priority { get; }
    public State(T c, int priority)
    {
        context = c;
        Priority = priority;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Update();
}

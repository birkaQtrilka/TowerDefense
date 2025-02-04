
[System.Serializable]
public abstract class State<T> : BaseState where T : IStateMachine
{
    protected T context;
    /// <summary>
    /// Used to not allow other states to appear in case their priority is lower
    /// </summary>
    public int Priority { get;  set; }
    public State(T c, int priority)
    {
        context = c;
        Priority = priority;
    }

    //empty constructor, so the activator can create it via reflection
    public State()
    {
        
    }

    public void Init(T context)
    {
        this.context = context;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Update();
}




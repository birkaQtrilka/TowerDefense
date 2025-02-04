
[System.Serializable]
public abstract class State<T> : State where T : IStateMachine
{
    protected T context;
    public int Priority { get;  set; }
    public State(T c, int priority)
    {
        context = c;
        Priority = priority;
    }
    
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
[System.Serializable]
public abstract class State
{

}

//make state serializable//
//have state with empty constructor//
//make factory create states with empty constructor during editor mode
//make GameManger initialize states

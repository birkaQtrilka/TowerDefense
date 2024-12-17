using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour, IStateMachine
{
    public static GameManager Instance { get; private set; }
    public State<GameManager> CurrentState { get; private set; }
    Dictionary<Type, State<GameManager>> _states;

    [field: SerializeField] public Store Store { get; private set; }
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
    [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
    [field: SerializeField] public TowerSelector TowerSelector { get; private set; }
    [field: SerializeField] public TowerPlacer TowerPlacer { get; private set; }

    public void TransitionToState(Type state)
    {
        CurrentState.OnExit();
        CurrentState = _states[state];
        CurrentState.OnEnter();
    }

    //for inspector button compatibility
    public void TransitionToState(string state) 
    {
        Type type = Type.GetType(state);
        TransitionToState(type);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _states = new()
        {
            { typeof(Build), new Build(this)},
            { typeof(Defend), new Defend(this)},
            { typeof(GameOver), new GameOver(this)},
        };

        
    }
    
    void Start()
    {
        CurrentState = _states[typeof(Build)];
        CurrentState.OnEnter();
    }

    public void Update()
    {
        CurrentState.Update();
    }
}

public interface IStateMachine
{

}

public abstract class State<T> where T : IStateMachine
{
    protected T context;
    public State(T c)
    {
        context = c;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Update();
}

public class Build : State<GameManager>
{
    public Build(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        context.Store.gameObject.SetActive(true);
        context.TowerPlacer.enabled = true;
        context.TowerSelector.enabled = true;

    }

    public override void OnExit()
    {
        context.Store.gameObject.SetActive(false);
        context.TowerPlacer.enabled = false;
        context.TowerSelector.Deselect();
        context.TowerSelector.enabled = false;

    }

    public override void Update()
    {

    }
}

public class Defend : State<GameManager>
{
    public Defend(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        context.EnemySpawner.StartSpawning();
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {

    }
}

public class GameOver : State<GameManager>
{
    public GameOver(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.GameOverUI.gameObject.SetActive(true);

    }

    public override void OnExit()
    {
        context.GameOverUI.gameObject.SetActive(false);

    }

    public override void Update()
    {

    }
}
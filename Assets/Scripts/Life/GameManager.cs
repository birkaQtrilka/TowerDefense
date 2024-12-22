using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStateMachine
{
    public static GameManager Instance { get; private set; }
    public State<GameManager> PreviousState { get; private set; }
    public State<GameManager> CurrentState { get; private set; }
    Dictionary<Type, State<GameManager>> _states;

    [field: SerializeField] public float BuildTime { get; private set; }
    [field: SerializeField] public BuildTimerUI BuildTimeUI { get; private set; }
    [field: SerializeField] public Store Store { get; private set; }
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
    [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
    [field: SerializeField] public GameWinUI GameWinUI { get; private set; }
    [field: SerializeField] public PauseMenuUI PauseMenuUI { get; private set; }
    [field: SerializeField] public TowerSelector TowerSelector { get; private set; }
    [field: SerializeField] public TowerPlacer TowerPlacer { get; private set; }
    [SerializeField, Range(0.0f, 1.0f)] float time = 1;
    public void TransitionToState(Type state)
    {
        CurrentState.OnExit();
        PreviousState = CurrentState;
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
            { typeof(GameWin), new GameWin(this)},
            { typeof(Pause), new Pause(this)},
        };
    }
    
    void Start()
    {
        CurrentState = _states[typeof(Build)];
        PreviousState = CurrentState;
        CurrentState.OnEnter();
    }

    public void Update()
    {
        Time.timeScale = time;
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState.GetType() != typeof(Pause))
        {
            TransitionToState(typeof(Pause));
            return;
        }

        CurrentState.Update();
        
    }

    void OnEnable()
    {
        EnemySpawner.WaveDone.AddListener(OnWaveEnded);
    }

    void OnDisable()
    {
        EnemySpawner.WaveDone.RemoveListener(OnWaveEnded);
        CurrentState.OnExit();
    }

    void OnWaveEnded(EnemySpawner spawner)
    {
        if (spawner.CurrentWave == spawner.TotalWaves)
            TransitionToState(typeof(GameWin));
        else
            TransitionToState(typeof(Build));
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
    float _buildingTimer;

    public Build(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        context.Store.gameObject.SetActive(true);
        context.TowerPlacer.enabled = true;
        context.TowerSelector.enabled = true;
        context.BuildTimeUI.gameObject.SetActive(true);
        _buildingTimer = context.BuildTime;
    }

    public override void OnExit()
    {
        if(context.Store != null)
            context.Store.gameObject.SetActive(false);
        if (context.TowerPlacer != null)
            context.TowerPlacer.enabled = false;
        if(context.TowerSelector != null)
            context.TowerSelector.Deselect();
        if (context.BuildTimeUI != null)
            context.BuildTimeUI.gameObject.SetActive(false);
        if (context.TowerSelector != null)
            context.TowerSelector.enabled = false;

    }

    public override void Update()
    {
        _buildingTimer -= Time.deltaTime;

        if(_buildingTimer <= 0)
            context.TransitionToState(typeof(Defend));
        else
            context.BuildTimeUI.ShowTime(_buildingTimer);

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
        if (context.GameOverUI != null)
            context.GameOverUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}

public class GameWin : State<GameManager>
{
    public GameWin(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.GameWinUI.gameObject.SetActive(true);
        LevelsManager.Instance.MarkNextLevelUnlocked();
    }

    public override void OnExit()
    {
        if (context.GameWinUI != null)
            context.GameWinUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}

public class Pause : State<GameManager>
{
    public Pause(GameManager c) : base(c) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.PauseMenuUI.gameObject.SetActive(true);

    }

    public override void OnExit()
    {
        if (context.PauseMenuUI != null)
            context.PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}
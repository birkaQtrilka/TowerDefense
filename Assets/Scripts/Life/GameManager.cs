using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour, IStateMachine
{
    public static GameManager Instance { get; private set; }
    public State<GameManager> PreviousState { get; private set; }
    public State<GameManager> CurrentState { get; private set; }
    Dictionary<Type, State<GameManager>> _states;

    [field: SerializeField] public float BuildTime { get; private set; }
    [field: SerializeField] public BuildTimerUI BuildTimeUI { get; private set; }//
    [field: SerializeField] public Store Store { get; private set; }
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
    [field: SerializeField] public GameOverUI GameOverUI { get; private set; }//
    [field: SerializeField] public GameWinUI GameWinUI { get; private set; }//
    [field: SerializeField] public PauseMenuUI PauseMenuUI { get; private set; }//
    [field: SerializeField] public TowerSelector TowerSelector { get; private set; }
    [field: SerializeField] public TowerPlacer TowerPlacer { get; private set; }//

    [SerializeField] bool _canChangeStates = true;
    [SerializeField, Range(0.0f, 2.0f)] float _time = 1;
    [SerializeField] string _startState = typeof(Build).Name;
    CancellationTokenSource _cancellationTokenSource;

    public void TransitionToState(Type state)
    {
        if (!_canChangeStates) return;


        var newState = _states[state];

        if (PreviousState.Priority > newState.Priority) return;

        Debug.Log("Changed game state to: " + newState);

        CurrentState.OnExit();
        PreviousState = CurrentState;
        CurrentState = newState;
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
            { typeof(Build), new Build(this,0)},
            { typeof(Defend), new Defend(this, 0)},
            { typeof(GameOver), new GameOver(this,999)},
            { typeof(GameWin), new GameWin(this,999)},
            { typeof(Pause), new Pause(this,0)},
        };
    }

    void Start()
    {
        Type type = Type.GetType(_startState);
        if (type == null)
        {
            Debug.LogError("No existing game state with that name (check spelling)");
        }

        CurrentState = _states[type];
        PreviousState = CurrentState;
        CurrentState.OnEnter();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState.GetType() != typeof(Pause))
        {
            TransitionToState(typeof(Pause));
        }

        CurrentState.Update();

    }

    async Task TimeScaleLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(100, token);
            _time = Time.timeScale;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _time = Mathf.Clamp(_time + 0.1f, 0.0f, 2.0f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _time = Mathf.Clamp(_time - .1f, 0.0f, 2.0f);
            }
            Time.timeScale = _time;
        }
    }
    void OnEnable()
    {
        EnemySpawner.WaveDone.AddListener(OnWaveEnded);
        _cancellationTokenSource = new CancellationTokenSource();
        _ = TimeScaleLoop(_cancellationTokenSource.Token);
    }

    void OnDisable()
    {
        EnemySpawner.WaveDone.RemoveListener(OnWaveEnded);
        CurrentState.OnExit();
        _cancellationTokenSource.Cancel();
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

public class Build : State<GameManager>
{
    float _buildingTimer;

    public Build(GameManager c, int prio) : base(c,prio) { }

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
    public Defend(GameManager c, int prio) : base(c, prio) { }

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
    public GameOver(GameManager c, int prio) : base(c, prio) { }

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
    public GameWin(GameManager c, int prio) : base(c, prio) { }

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
    public Pause(GameManager c, int prio) : base(c, prio) { }

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
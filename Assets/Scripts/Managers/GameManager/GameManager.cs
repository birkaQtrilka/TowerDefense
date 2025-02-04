using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
//Manages Game states
//[RequireComponent()]
public class GameManager : MonoBehaviour, IStateMachine
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public bool CanChangeTime { get; set; } = false;

    //to allow backtracking
    public State<GameManager> PreviousState { get; private set; }
    public State<GameManager> CurrentState { get; private set; }
    Dictionary<Type, State<GameManager>> _states;

    //these are Properties needed by the states
    [field: SerializeField] public float BuildTime { get; private set; }
    [field: SerializeField] public BuildTimerUI BuildTimeUI { get; private set; }
    [field: SerializeField] public Store Store { get; private set; }
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
    [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
    [field: SerializeField] public GameWinUI GameWinUI { get; private set; }
    [field: SerializeField] public PauseMenuUI PauseMenuUI { get; private set; }
    [field: SerializeField] public TowerSelector TowerSelector { get; private set; }
    [field: SerializeField] public TowerPlacer TowerPlacer { get; private set; }

    //for testing
    [SerializeField] bool _canChangeStates = true;
    [SerializeField, Range(0.0f, 3.0f)] float _time = 1;
    [SerializeField] string _startState = typeof(Build).Name;

    //this is to cancel the async loop to prevent it from playing outside the scene
    CancellationTokenSource _updateCancelToken;

    public void TransitionToState(Type state)
    {
        if (!_canChangeStates) return;


        var newState = _states[state];

        if (CurrentState.Priority > newState.Priority) return;

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

        
    }

    void Start()
    {
        _states ??= new()
        {
            { typeof(Build), new Build(this,0)},
            { typeof(Defend), new Defend(this, 0)},
            { typeof(GameOver), new GameOver(this,999)},
            { typeof(GameWin), new GameWin(this,999)},
            { typeof(Pause), new Pause(this,0)},
        };

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

    public void SetStates(Dictionary<Type, State<GameManager>> states)
    {
        _states = states;
    }

    //it's async so it doesn't stop on timescale = 0
    async Task TimeScaleLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {

            _time = Time.timeScale;
            await Task.Delay(20, token);
            
            if (!CanChangeTime) continue;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _time = Mathf.Clamp(_time + 0.2f, 0.0f, 3.0f);
                await Task.Delay(50, token);

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _time = Mathf.Clamp(_time - .2f, 0.0f, 3.0f);
                await Task.Delay(50, token);

            }
            Time.timeScale = _time;
        }
    }

    void OnEnable()
    {
        EnemySpawner.WaveDone.AddListener(OnWaveEnded);
        _updateCancelToken = new CancellationTokenSource();
        _ = TimeScaleLoop(_updateCancelToken.Token);
    }

    void OnDisable()
    {
        EnemySpawner.WaveDone.RemoveListener(OnWaveEnded);
        CurrentState.OnExit();
        _updateCancelToken.Cancel();
    }

    void OnWaveEnded(EnemySpawner spawner)
    {
        if (spawner.CurrentWave == spawner.TotalWaves)
            TransitionToState(typeof(GameWin));
        else
            TransitionToState(typeof(Build));
    }

}

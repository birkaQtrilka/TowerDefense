using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles attack Waves and the spawn of enemies
/// </summary>
public abstract class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<EnemySpawner> WaveDone {  get; private set; }

    //current wave, max waves
    [field: SerializeField] public UnityEvent<int, int> ValuesChanged { get; private set; }

    [SerializeField] protected PathingManager pathingManager;
    [SerializeField] WavesAsset _wavesContainer;
    EnemyWave[] _waves;

    public int CurrentWave { get; protected set; }
    public int TotalWaves => _waves.Length;
    /// <summary>
    /// to prevent recalculating the position on every enemy spawn
    /// </summary>
    Vector3 _cahcedStartPosition;
    //keeping track to know when to finish the level
    readonly List<Enemy> _spawnedEnemies = new();

    void Awake()
    {
        _waves = _wavesContainer.GetWaves();
    }

    /// <summary>
    /// starts up the wave
    /// </summary>
    public void StartSpawning()
    {
        _cahcedStartPosition = pathingManager.GetStartPosition();

        StopAllCoroutines();
        StartCoroutine(SpawnWavesCR());
    }

    /// <summary>
    /// decides the order and timing of enemyspawns within a wave
    /// </summary>
    /// <param name="wave"></param>
    /// <returns></returns>
    protected abstract IEnumerator SpawnWave(EnemyWave wave);

    /// <summary>
    /// responsible for initializing the Enemy
    /// </summary>
    /// <param name="prefab"></param>
    protected virtual void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, _cahcedStartPosition, Quaternion.identity);
        _spawnedEnemies.Add(enemy);
        enemy.OnDeath.AddListener(OnEnemyDeath);
        enemy.OnDestroy.AddListener(OnEnemyDeath);
        IMover walkingBehavior = enemy.GetComponent<IMover>();

        walkingBehavior.SetDestination(pathingManager.GetDestination());
        walkingBehavior.MoveToDestination();
    }


    void OnEnemyDeath(Enemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
        enemy.OnDestroy.RemoveListener(OnEnemyDeath);
    }

    /// <summary>
    /// responsible to set the pause between waves
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWavesCR()
    {
        if (CurrentWave > _waves.Length - 1) yield break;

        yield return SpawnWave(_waves[CurrentWave]);
        CurrentWave++;
        ValuesChanged?.Invoke(CurrentWave, _waves.Length);
        
        yield return new WaitUntil(()=> _spawnedEnemies.Count == 0);

        WaveDone?.Invoke(this);
    }


}

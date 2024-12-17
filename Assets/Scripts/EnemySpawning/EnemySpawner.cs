using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class EnemySpawner : MonoBehaviour
{
    //curr, max
    [field: SerializeField] public UnityEvent<int, int> ValuesChanged { get; private set; }

    [SerializeField] protected PathingManager pathingManager;
    [SerializeField] protected EnemyWave[] waves;

    public int CurrentWave => currWave;
    public int TotalWaves => waves.Length;

    protected int currWave;
    Coroutine currWaveRoutine;

    Vector3 _cahcedStartPosition;

    List<Enemy> _spawnedEnemies = new();

    public void StartSpawning()
    {
        _cahcedStartPosition = pathingManager.GetStartPosition();
        StartCoroutine(SpawnWavesCR());
    }

    public void RushNewWave()
    {
        if (currWave > waves.Length - 1) return;

        currWaveRoutine = StartCoroutine(SpawnWave(waves[currWave]));
        currWave++;
        ValuesChanged?.Invoke(currWave, waves.Length);
    }

    //this decides the order and timing of enemyspawns within a wave
    protected abstract IEnumerator SpawnWave(EnemyWave wave);

    protected virtual void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, _cahcedStartPosition, Quaternion.identity);
        _spawnedEnemies.Add(enemy);
        enemy.OnDeath.AddListener(OnEnemyDeath);
        IMover walkingBehavior = enemy.GetComponent<IMover>();

        walkingBehavior.SetDestination(pathingManager.GetDestination());
        walkingBehavior.MoveToDestination();
    }

    void OnEnemyDeath(Enemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
    }

    IEnumerator SpawnWavesCR()
    {
        while(currWave < waves.Length)
        {
            RushNewWave();
            yield return currWaveRoutine;
            //yield return new WaitForSeconds(waves[currWave-1].Pause);
        }
        yield return new WaitUntil(()=> _spawnedEnemies.Count == 0);
        Debug.Log("enemy spawner transition");
        GameManager.Instance.TransitionToState(typeof(GameOver));
    }


}

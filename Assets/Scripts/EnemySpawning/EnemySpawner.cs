using System.Collections;
using UnityEngine;


public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected PathingManager _pathingManager;
    [SerializeField] protected EnemyWave[] waves;

    Vector3 _cahcedStartPosition;

    public void StartSpawning()
    {
        _cahcedStartPosition = _pathingManager.GetStartPosition();
        StartCoroutine(SpawnWaves());
    }

    protected abstract IEnumerator SpawnWaves();

    protected virtual void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, _cahcedStartPosition, Quaternion.identity);

        IMover walkingBehavior = enemy.GetComponent<IMover>();

        walkingBehavior.SetDestination(_pathingManager.GetDestination());
        walkingBehavior.MoveToDestination();
    }

    
}

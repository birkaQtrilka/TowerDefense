using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PathingManager _pathingManager;
    [SerializeField] EnemyWave[] _waves;

    Vector3 _cahcedStartPosition;

    void Start()
    {
        //_ = SpawnWaves();
        StartCoroutine(SpawnWavesCr());
    }

    IEnumerator SpawnWavesCr()
    {
        foreach (var wave in _waves)
        {
            _cahcedStartPosition = _pathingManager.GetStartPosition();

            foreach (EnemyWave.EnemySet set in wave.GetEnemySets())
            {
                for (int i = 0; i < set.Amount; i++)
                {
                    SpawnEnemy(set.EnemyPrefab);
                    yield return new WaitForSeconds(set.Pause);
                }
            }
            yield return new WaitForSeconds(wave.Pause);
        }
    }

    void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, _cahcedStartPosition, Quaternion.identity);

        IWalker walkingBehavior = enemy.GetComponent<IWalker>();

        walkingBehavior.SetDestination(_pathingManager.GetDestination());
        walkingBehavior.WalkToDestination();
    }

    
}

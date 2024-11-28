using System.Collections;
using UnityEngine;

public class LinearEnemySpawner : EnemySpawner
{
    [SerializeField] bool _spawn;

    protected override IEnumerator SpawnWaves()
    {
        foreach (var wave in waves)
        {
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

    private void Update()
    {
        if (_spawn)
        {
            _spawn = false;
            StartSpawning();
        }
    }
}

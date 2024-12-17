using System.Collections;
using UnityEngine;

public class LinearEnemySpawner : EnemySpawner
{
    [SerializeField] bool _spawn;
    
    protected override IEnumerator SpawnWave(EnemyWave wave)
    {
        foreach (EnemyWave.EnemySet set in wave.GetEnemySets())
        {
            for (int i = 0; i < set.Amount; i++)
            {
                SpawnEnemy(set.EnemyPrefab);
                yield return new WaitForSeconds(set.Pause);
            }
        }

    }

    private void Update()
    {
        if (_spawn)
        {
            _spawn = false;
            RushNewWave();
        }
    }
}

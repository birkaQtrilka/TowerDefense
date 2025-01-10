using System.Collections;
using UnityEngine;
/// <summary>
/// simpli spawns every enemy in order 
/// </summary>
public class LinearEnemySpawner : EnemySpawner
{
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

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongFirstEnemySpawner : EnemySpawner
{
    protected override IEnumerator SpawnWave(EnemyWave wave)
    {
        var sets = wave.GetEnemySetsCopy();
        SortSets(sets);

        foreach (EnemyWave.EnemySet set in sets)
        {
            for (int i = 0; i < set.Amount; i++)
            {
                SpawnEnemy(set.EnemyPrefab);
                yield return new WaitForSeconds(set.Pause);
            }
        }
    }

    void SortSets(EnemyWave.EnemySet[] sets)
    {
        Array.Sort(sets,
                Comparer<EnemyWave.EnemySet>.Create((a, b) =>
                    a.EnemyPrefab.GetHealth().CurrentValue
                    .CompareTo(
                    b.EnemyPrefab.GetHealth().CurrentValue)
                ));
    }
}

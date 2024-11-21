using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct EnemyWave //should it be a scriptable object?
{
    [Serializable]
    public struct EnemySet
    {
        public int Amount;
        public Enemy EnemyPrefab;
        public float Pause;
    }

    [SerializeField] EnemySet[] _enemySets;
    [field: SerializeField] public float Pause { get; private set; }
    //public readonly List<Enemy> GetEnemies()
    //{
    //    List<Enemy> list = new();
    //    foreach (EnemySet set in _enemySets) 
    //    {
    //        for (int i = 0; i < set.Amount; i++)
    //            list.Add(set.EnemyPrefab);
    //    }

    //    return list;
    //}

    public EnemySet[] GetEnemySets()
    {
        return _enemySets;
    }
    //other data abt wave
}



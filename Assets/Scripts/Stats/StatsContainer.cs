
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class StatsContainer
{
    [SerializeReference] List<Stat> stats;

    public T GetStat<T>() where T : Stat
    {
        var type = typeof(T);
        return stats.FirstOrDefault(s => s.GetType() == type) as T;
        //if(_statsDictionary == null)

    }

    //void BuildDictionary()
    //{
    //    _statsDictionary = new();

    //    foreach (Stat stat in stats) 
    //    {
    //        _statsDictionary.Add(stat.GetType(), stat);
    //    }
    //}

    public void DoForEach(Action<Stat> method)
    {
        foreach (Stat stat in stats)
            method(stat);
    }
}
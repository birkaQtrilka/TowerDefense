using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

//just a wrapper class to be able to have a custom property drawer
//apparently you cannot have something like CustomPropertyDrower(typeof(List<Stat>))
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

    public void DoForEach(Action<Stat> method)
    {
        foreach (Stat stat in stats)
            method(stat);
    }
}

using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public abstract class Stat<T> : Stat
{
    public abstract T Value { get; protected set; }
}
[System.Serializable]
public abstract class Stat
{

}

[Serializable]
public class StatsContainer
{
    [SerializeReference] public List<Stat> stats;
}
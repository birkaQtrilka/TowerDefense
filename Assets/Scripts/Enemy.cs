using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof(IMover))]
public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    [SerializeField] StatsContainer _stats;
    IMover _walker;

    Health _health;

    void Awake()
    {
        _walker = GetComponent<IMover>();
        _health = _stats.GetStat<Health>();
    }
    
    public Health GetHealth()
    {
        return _health;
    }
}

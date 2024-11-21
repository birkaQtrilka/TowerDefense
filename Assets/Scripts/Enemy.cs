using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof(IWalker))]
public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    [SerializeField] StatsContainer _stats;
    IWalker _walker;

    void Awake()
    {
        _walker = GetComponent<IWalker>();
    }
    

}

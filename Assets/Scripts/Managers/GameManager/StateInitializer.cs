using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for injecting data that is only alive in the scene (runtime)
/// </summary>
[RequireComponent(typeof(GameManager))]
public class StateInitializer : MonoBehaviour
{
    [SerializeField] GameManagerReflectionFactory _reflectionFactory;

    void Awake()
    {
        GameManager manager = GetComponent<GameManager>();

        var states = _reflectionFactory.GetInitializedStates();
        InjectManagerDependency(states, manager);
        manager.SetStates(states);
    }

    void InjectManagerDependency(Dictionary<Type, State<GameManager>> states, GameManager manager)
    {
        foreach (var state in states.Values) 
        {
            state.Init(manager);
        }
    }
}

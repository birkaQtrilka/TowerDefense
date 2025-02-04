using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class StateInitializer : MonoBehaviour
{
    //get factory dictionary
    //create serializable list for inspector
    //initialize based of the list
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

[Serializable]
public class StateInitData
{
    [InspectorReadOnly]
    public string Name;

    public int Priority;


    
}
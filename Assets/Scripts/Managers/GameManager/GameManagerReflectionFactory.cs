using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Creates the states via reflection and allows data to be set via inspector
/// </summary>
[CreateAssetMenu(menuName = "Stefan/StateFactory/UsingReflection")]
public class GameManagerReflectionFactory : ReflectionStateFactory<GameManager>
{
    [SerializeField] bool _getInitData;
    [SerializeField] List<StateInitData> _initData;
#if UNITY_EDITOR
    void OnValidate()
    {
        if (_getInitData)
        {
            _getInitData = false;

            InitStates(GetStates());
        }
    }
#endif
    void InitStates(Dictionary<Type, State<GameManager>> states)
    {
        _initData.Clear();
        foreach (Type type in states.Keys)
        {
            _initData.Add(new StateInitData() { Priority = 0, Name = type.Name });
        }
    }

    public Dictionary<Type, State<GameManager>> GetInitializedStates() 
    {
        Dictionary<Type, State<GameManager>> states = GetStates();

        if (states.Count != _initData.Count)
        {
            Debug.LogError("You have not updated the initialization data in state initializer, all the values will be defaulted");
            InitStates(states);
        }

        for (int i = 0; i < _initData.Count; i++)
        {
            StateInitData stateInitData = _initData[i];

            states[Type.GetType(stateInitData.Name)].Priority = stateInitData.Priority;
        }
        return states;
    }
}

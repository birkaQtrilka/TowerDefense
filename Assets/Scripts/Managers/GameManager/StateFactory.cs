using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Responsible for creating the states for any state machine
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateFactory<T> : ScriptableObject where T : IStateMachine
{
    public abstract Dictionary<Type, State<T>> GetStates();
}

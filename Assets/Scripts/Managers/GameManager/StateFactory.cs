using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateFactory<T> : ScriptableObject where T : IStateMachine
{
    public abstract Dictionary<Type, State<T>> GetStates();
}

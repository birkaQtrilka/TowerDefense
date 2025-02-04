using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ReflectionStateFactory<T> : StateFactory<T> where T : IStateMachine
{
    public override Dictionary<Type, State<T>> GetStates()
    {
        Type abstractClass = typeof(BaseState);

        return Assembly.GetAssembly(abstractClass)
            .GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(abstractClass))
            .ToDictionary(t => t, t => Activator.CreateInstance(t) as State<T>);
    }
}

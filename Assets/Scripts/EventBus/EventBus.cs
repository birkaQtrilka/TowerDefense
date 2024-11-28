using System;

public class EventBus<T> where T : IEvent
{
    public static event Action<T> Invoked;

    public static void Publish(T args)
    {
        Invoked?.Invoke(args);
    }
}

public interface IEvent { }

public class TowerSelected : IEvent 
{
    public Tower Tower { get; }

    public TowerSelected(Tower tower)
    {
        Tower = tower;
    }


    
}


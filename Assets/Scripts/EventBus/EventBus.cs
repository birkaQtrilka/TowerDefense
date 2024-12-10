using System;

public class EventBus<T> where T : IEvent
{
    public static event Action<T> Event;

    public static void Publish(T args)
    {
        Event?.Invoke(args);
    }
}

public interface IEvent { }

public readonly struct MoneySpent : IEvent
{
    public int Amount { get; }
    public int NewTotal { get; }
    public MoneySpent(int amount, int money)
    {
        Amount = amount;
        NewTotal = money;
    }
}


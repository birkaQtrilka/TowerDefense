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

public readonly struct TryBuy : IEvent
{
    public int Amount { get; }
    public Action OnAllow { get; }

    public TryBuy(int amount, Action onAllow)
    {
        Amount = amount;
        OnAllow = onAllow;
    }
}

public readonly struct TowerUpgraded : IEvent
{
    public TowerUpgrader OldUpgrader { get; }
    public TowerUpgrader CurrentUpgrader { get; }

    public TowerUpgraded(TowerUpgrader oldUpgrader, TowerUpgrader currentUpgrader)
    {
        OldUpgrader = oldUpgrader;
        CurrentUpgrader = currentUpgrader;
    }
    
}
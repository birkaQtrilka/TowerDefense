using System;
/// <summary>
/// The publisher is creating this event, and the listener will Call OnAllow
/// </summary>
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

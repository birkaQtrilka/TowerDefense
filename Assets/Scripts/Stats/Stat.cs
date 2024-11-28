[System.Serializable]
public abstract class Stat<T> : Stat
{
    public abstract T CurrentValue { get; protected set; }
    public abstract T OriginalValue { get; protected set; }
    public abstract T MaxValue { get; protected set; }

    public override string ToString()
    {
        return OriginalValue.ToString();
    }
}

[System.Serializable]
public abstract class Stat
{

}

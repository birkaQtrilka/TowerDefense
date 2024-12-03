
public class Damage : Stat<int>
{
    public DamageType Type;
    public override void OnCurrentValueSet(ref int val)
    {
        if (val < 0) val = 0;
    }
}
public enum DamageType
{
    Fire,
    Ice,
    Water
}
[System.Serializable]
public class Damage : Stat<int>
{
    public DamageType Type;
    protected override void OnCurrentValueSet(ref int val)
    {
        if (val < 0) val = 0;
    }
}

using UnityEngine;

public class CarriedMoney : Stat<int>
{
    [field: SerializeField] public override int CurrentValue  { get; protected set; }
    [field: SerializeField] public override int OriginalValue { get; protected set; }
    [field: SerializeField] public override int MaxValue      { get; protected set; }

}

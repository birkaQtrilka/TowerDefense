using UnityEngine;

public class Speed : Stat<float>
{
    [SerializeField] float _value;
    public override float CurrentValue { get => _value; protected set => _value = value; }
    [field: SerializeField] public override float OriginalValue {get;protected set;}
    [field: SerializeField] public override float MaxValue { get; protected set; }
}

using UnityEngine;
[System.Serializable]
public class Health : Stat<int>
{
    [SerializeField] int _currentValue;
    public override int CurrentValue { get => _currentValue; protected set => _currentValue = value; }
    [field: SerializeField] public override int OriginalValue { get; protected set ; }
    [field: SerializeField] public override int MaxValue { get; protected set; }
}

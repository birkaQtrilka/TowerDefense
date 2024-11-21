using UnityEngine;

public class Speed : Stat<float>
{
    [SerializeField] float _value;
    public override float Value { get => _value; protected set => _value = value; }
}

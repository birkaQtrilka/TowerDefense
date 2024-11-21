using UnityEngine;
[System.Serializable]
public class Health : Stat<int>
{
    [SerializeField] int _value;
    public override int Value { get => _value; protected set => _value = value; }
}

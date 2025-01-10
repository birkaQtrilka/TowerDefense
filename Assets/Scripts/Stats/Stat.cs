using UnityEngine;
using UnityEngine.Events;
//to allow polymorphic lists in the inspector
[System.Serializable]
public abstract class Stat
{

}

[System.Serializable]
public abstract class Stat<T> : Stat
{
    //previous and current
    [field: EventFoldout][field: SerializeField] public UnityEvent<T,Stat<T>> CurrentUpdated { get; private set; }
    [field: EventFoldout][field: SerializeField] public UnityEvent<T, Stat<T>> OriginalUpdated { get; private set; }
    [field: EventFoldout][field: SerializeField] public UnityEvent<T, Stat<T>> MaxUpdated { get; private set; }

    [SerializeField] T _currentValue; 
    [SerializeField] T _originalValue; 
    [SerializeField] T _maxValue; 

    public T CurrentValue {
        get
        {
            OnCurrentValueGet(ref _currentValue);
            return _currentValue;
        }
        set 
        {
            OnCurrentValueSet(ref value);
            var copy = _currentValue;
            _currentValue = value;
            CurrentUpdated?.Invoke(copy, this);
        }
    }

    public T OriginalValue
    {
        get
        {
            OnOriginalValueGet(ref _originalValue);
            return _originalValue;
        }
        set
        {
            OnOriginalValueSet(ref value);
            var copy = _originalValue;
            _originalValue = value;
            OriginalUpdated?.Invoke(copy, this);
        }
    }

    public T MaxValue
    {
        get
        {
            OnMaxValueGet(ref _maxValue);
            return _originalValue;
        }
        set
        {
            OnMaxValueSet(ref value);
            var copy = _maxValue;
            _maxValue = value;
            MaxUpdated?.Invoke(copy, this);
        }
    }
    //a way to solve having default properties and also allow for polymorphism
    protected virtual void OnCurrentValueGet(ref T val) {}
    protected virtual void OnOriginalValueGet(ref T val) { }
    protected virtual void OnMaxValueGet(ref T val) { }

    protected virtual void OnCurrentValueSet(ref T val) { }
    protected virtual void OnOriginalValueSet(ref T val) { }
    protected virtual void OnMaxValueSet(ref T val) { }

    public override string ToString()
    {
        return OriginalValue.ToString();
    }
}


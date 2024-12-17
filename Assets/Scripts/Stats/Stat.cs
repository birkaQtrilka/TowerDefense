using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class Stat<T> : Stat
{
    //previous and current
    [EventFoldout] public UnityEvent<T,T> OnCurrentUpdate;
    [EventFoldout] public UnityEvent<T,T> OnOriginalUpdate;
    [EventFoldout] public UnityEvent<T,T> OnMaxUpdate;

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
            OnCurrentUpdate?.Invoke(copy, value);
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
            OnOriginalUpdate?.Invoke(copy, value);
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
            OnMaxUpdate?.Invoke(copy, value);
        }
    }

    public virtual void OnCurrentValueGet(ref T val) {}
    public virtual void OnOriginalValueGet(ref T val) { }
    public virtual void OnMaxValueGet(ref T val) { }

    public virtual void OnCurrentValueSet(ref T val) { }
    public virtual void OnOriginalValueSet(ref T val) { }
    public virtual void OnMaxValueSet(ref T val) { }

    public override string ToString()
    {
        return OriginalValue.ToString();
    }
}

[System.Serializable]
public abstract class Stat
{

}

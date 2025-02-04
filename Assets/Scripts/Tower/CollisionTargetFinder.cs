using System.Collections.Generic;
using UnityEngine;

public class CollisionTargetFinder : TargetFinder
{
    readonly Queue<Transform> _targets = new();
    SphereCollider _collider;
    
    //[SerializeField] List<Transform> _test;

    public override float Range 
    {
        get
        {
            if (_collider == null) _collider = GetComponent<SphereCollider>();

            return _collider.radius;
        }
        set
        {
            if (_collider == null) _collider = GetComponent<SphereCollider>();
            _collider.radius = value;
        }
    }

    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public override IEnumerable<Transform> GetAvailableTargets()
    {
        CleanUpQueue();
        return _targets;
    }
    
    //adapter for stat value event signature
    public void SetRange(float oldVal, Stat<float> range)
    {
        Range = range.CurrentValue;
    }

    void Update()
    {
        CleanUpQueue();
        //_test = new(_targets);
    }

    public override Transform GetSingleTarget()
    {
        //apparently ontrigger exit is not called when the object is destroyed so I need to cleanup the queue
        CleanUpQueue();

        if (_targets.Count == 0)
            return null;
        return _targets.Peek();
    }

    void OnTriggerEnter(Collider other)
    {
        _targets.Enqueue(other.transform);
    }
    

    void OnTriggerExit(Collider other)
    {
        //apparently ontrigger exit is not called when the object is destroyed so I need to cleanup the queue
        CleanUpQueue();

        if (_targets.Count != 0)
            _targets.Dequeue();

        CleanUpQueue();

    }

    void CleanUpQueue()
    {
        while (_targets.Count != 0 && _targets.Peek() == null)
            _targets.Dequeue();
    }
}

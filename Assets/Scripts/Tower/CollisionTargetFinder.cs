using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionTargetFinder : TargetFinder
{
    readonly Queue<Transform> _targets = new();
    SphereCollider _collider;
    public override float Range { get => _collider.radius; set => _collider.radius = value; }
    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public override IEnumerable<Transform> GetAvailableTargets()
    {
        return _targets;
    }
    
    //adapter for stat value event signature
    public void SetRange(float oldVal, Stat<float> range)
    {
        Range = range.CurrentValue;
    }

    public override Transform GetSingleTarget()
    {
        //apparently ontrigger exit is not called when the object is destroyed so I need to cleanup the queue
        while (_targets.Count != 0 && _targets.Peek() == null)
            _targets.Dequeue();
        if(_targets.Count == 0)
            return null;
        return _targets.Peek();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered " + other.gameObject);
        _targets.Enqueue(other.transform);
    }
    

    void OnTriggerExit(Collider other)
    {
        //apparently ontrigger exit is not called when the object is destroyed so I need to cleanup the queue
        //Debug.Log("exited " + other.gameObject);
        while(_targets.Count != 0 && _targets.Peek() == null)
           _targets.Dequeue();
        if (_targets.Count != 0)
            _targets.Dequeue();

        while (_targets.Count != 0 && _targets.Peek() == null)
            _targets.Dequeue();
    }
}

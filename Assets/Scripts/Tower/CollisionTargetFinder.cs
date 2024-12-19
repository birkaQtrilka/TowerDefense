using System.Collections.Generic;
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
        if (_targets.Count == 0) return null;
        return _targets.Peek();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy")) return;
        Debug.Log("entered " + other.gameObject);
        _targets.Enqueue(other.transform);
    }
    void Update()
    {
        if(_targets.Count == 0) return;
        if(_targets.Peek() == null) 
            _targets.Dequeue();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        Debug.Log("exited " + other.gameObject);

        _targets.Dequeue();
    }
}

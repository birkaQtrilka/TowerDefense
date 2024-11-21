using System.Collections.Generic;
using UnityEngine;

public class CollisionTargetFinder : TargetFinder
{
    readonly Stack<Transform> _targets = new();

    public override IEnumerable<Transform> GetAvailableTargets()
    {
        return _targets;
    }

    public override Transform GetSingleTarget()
    {
        if (_targets.Count == 0) return null;
        return _targets.Peek();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy")) return;
        _targets.Push(other.transform);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        _targets.Pop();
    }
}

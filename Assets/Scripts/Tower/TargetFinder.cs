using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    public abstract Transform GetSingleTarget();
    public abstract IEnumerable<Transform> GetAvailableTargets();
}

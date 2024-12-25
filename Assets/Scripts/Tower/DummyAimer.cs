using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DummyAimer : Aimer
{
    public override Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets)
    {
        Transform firstTarget = null;
        foreach (Transform t in targets) 
            if(t != null)
                firstTarget = t;
        if(firstTarget == null) return DefaultRotation;
        
        return  Quaternion.LookRotation((firstTarget.position - transform.position).normalized);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class DummyAimer : Aimer
{
    public override Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets)
    {
        //if(bullet is Projectile)
        //{
            var enumer = targets.GetEnumerator();
            if (!enumer.MoveNext()) return Quaternion.identity;
            var firstTarget = enumer.Current;

            return  Quaternion.LookRotation((firstTarget.position - transform.position).normalized);
        //}

        //return Quaternion.identity;
    }
}

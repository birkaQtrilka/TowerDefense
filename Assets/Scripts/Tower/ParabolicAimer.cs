using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class ParabolicAimer : Aimer
    {
        public override Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets)
        {
            Transform firstTarget = null;
            foreach (Transform t in targets)
                if (t != null)
                    firstTarget = t;
            if (firstTarget == null) return Quaternion.identity;

            //have the parabola

            return Quaternion.LookRotation((firstTarget.position - transform.position).normalized);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AheadAimer : Aimer
{
    Transform _cachedTarget;
    IMover _mover;

    public override Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets)
    {
        Transform firstTarget = null;
        foreach (Transform t in targets)
            if (t != null)
                firstTarget = t;
        if (firstTarget == null) return Quaternion.identity;
        //every frame? not good;
        if(firstTarget != _cachedTarget)
        {
            _cachedTarget = firstTarget;
            _mover = firstTarget.GetComponent<IMover>();
        }
        var u = firstTarget.position - transform.position;
        var a = Mathf.Pow(_mover.Velocity.magnitude, 2) - Mathf.Pow((bullet as Projectile).Velocity.magnitude, 2);
        var b = 2 * Vector3.Dot(_mover.Velocity, u);
        var c = Mathf.Pow(u.magnitude, 2);

        var delta = b * b - 4 * a * c;
        if (delta > 0)
        {
            var t = (-b - Mathf.Sqrt(delta)) / (2 * a);
            var angle = u + t * _mover.Velocity;
            return Quaternion.LookRotation(angle, Vector3.Cross(transform.right, angle));
            //return transform.position + angle.normalized;
        }
        return Quaternion.identity;
    }
}

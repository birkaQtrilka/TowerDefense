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
        if (firstTarget == null) return DefaultRotation;
        //every frame? not good;
        if(firstTarget != _cachedTarget)
        {
            _cachedTarget = firstTarget;
            _mover = firstTarget.GetComponentInParent<IMover>();
        }


        Vector3 moverXZvelocity = _mover.Velocity;
        moverXZvelocity.y = 0f;

        Vector3 dir = firstTarget.position - transform.position;
        float a = Mathf.Pow(moverXZvelocity.magnitude, 2) - Mathf.Pow((bullet as Projectile).StartSpeed, 2);
        float b = 2 * Vector3.Dot(moverXZvelocity, dir);
        float c = Mathf.Pow(dir.magnitude, 2);

        float delta = b * b - 4 * a * c;


        if (delta > 0)
        {
            float t = (-b - Mathf.Sqrt(delta)) / (2 * a);
            Vector3 angle = dir + t * moverXZvelocity;
            Debug.DrawRay(transform.position, angle, Color.yellow, 1);
            return Quaternion.LookRotation(angle, Vector3.Cross(transform.right, angle));
            //return transform.position + angle.normalized;
        }
        return DefaultRotation;
    }
}

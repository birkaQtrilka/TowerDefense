using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class ParabolicAimer : Aimer
    {
        [SerializeField] float height;

        public override Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets)
        {
            Transform firstTarget = null;
            foreach (Transform t in targets)
                if (t != null)
                    firstTarget = t;
            if (firstTarget == null) return Quaternion.identity;

            //have the parabola

            //bullet needs to be projectile 
            //get speed of bullet
            //get gravity
            //get distance to target
            //need to set the angle 
            float angle = CalculateAimAngle(transform.position, firstTarget.position, (bullet as Projectile).StartSpeed);

            Vector3 axis = Vector3.Cross((firstTarget.position - transform.position).normalized, Vector3.up);
            Debug.DrawRay(transform.position, axis, Color.yellow,1);
            
            return Quaternion.AngleAxis(angle, axis);
        }

        public float CalculateAimAngle(Vector3 startPosition, Vector3 endPosition, float bulletSpeed)
        {
            Vector3 delta = endPosition - startPosition;
            float x = delta.magnitude; // Horizontal distance
            float y = delta.y; // Vertical distance

            float vSquared = bulletSpeed * bulletSpeed;
            float g = 9.81f;

            // Calculate discriminant to check for valid solution
            float discriminant = (vSquared * vSquared) - (g * (g * x * x + 2 * y * vSquared));

            if (discriminant < 0)
            {
                Debug.LogError("No valid angle for the given parameters.");
                return -1; // No valid solution
            }

            // Solve for the angle using the quadratic formula
            float angle1 = Mathf.Atan((vSquared + Mathf.Sqrt(discriminant)) / (g * x));
            //float angle2 = Mathf.Atan((vSquared - Mathf.Sqrt(discriminant)) / (g * x));

            // Choose one of the angles based on preference
            return angle1 * Mathf.Rad2Deg; // Return the angle in degrees
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public abstract class Aimer : MonoBehaviour
{
    public Quaternion DefaultRotation { get; set; } = Quaternion.identity;
    public abstract Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets);
    
}

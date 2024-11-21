using System.Collections.Generic;
using UnityEngine;

public abstract class Aimer : MonoBehaviour
{
    public abstract Quaternion GetAttackLook(Bullet bullet, IEnumerable<Transform> targets);
    
}

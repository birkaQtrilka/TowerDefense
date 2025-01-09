using UnityEngine;

public class ImmunityCondition : MonoBehaviour, IDamageCondition
{

    public bool CanDamage(Tower tower, Enemy enemy)
    {
        return Debugging.Instance == null || !Debugging.Instance.HealthImmunity;
    }
}

using UnityEngine;
//for testing
public class InfiniteModifier : MonoBehaviour, IDamageModifier
{
    public int Modify(BulletDamager damager)
    {
        return Debugging.Instance != null && Debugging.Instance.InstaKill ? 999999 : 0;
    }

}

using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletDamager : MonoBehaviour
{
    public Damage Damage;
    
    Bullet _bullet;
    void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    void OnEnable()
    {
        //On hit automatically remvoes all listeners, no need for onDisable decoupling
        _bullet.OnHit.AddListener(DoDamage);
    }

    void DoDamage(Tower sender, Enemy victim)
    {
        //get tower damage stat?

        //CAN DO:
        //if enemy is water type and tower damage is fire type, do no damage etc.
        victim.GetHealth().CurrentValue -= Damage.CurrentValue;
    }
}

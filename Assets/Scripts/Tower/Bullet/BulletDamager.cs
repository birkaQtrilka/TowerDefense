using System.Linq;
using UnityEngine;
/// <summary>
/// Does damage to enemy
/// </summary>
[RequireComponent(typeof(Bullet))]
public class BulletDamager : MonoBehaviour
{
    public Damage Damage;
    //every condition must be true
    IDamageCondition[] _conditions;
    //modifiers are added up to then get the final damage
    IDamageModifier[] _modifiers;
    Bullet _bullet;
    void Awake()
    {
        _bullet = GetComponent<Bullet>();
        _conditions = GetComponents<IDamageCondition>();
        _modifiers = GetComponents<IDamageModifier>();
    }

    void OnEnable()
    {
        _bullet.OnHit.AddListener(DoDamage);
    }

    void OnDisable()
    {
        if (_bullet != null)
            _bullet.OnHit?.RemoveListener(DoDamage);
    }

    void DoDamage(Tower sender, Enemy victim)
    {
        //CAN DO:
        //if enemy is water type and tower damage is fire type, do no damage etc.
        if (_conditions.All(c => c.CanDamage(sender, victim)))
        {
            int damageTotal = Damage.CurrentValue;
            foreach (IDamageModifier modifier in _modifiers)
            {
                damageTotal += modifier.Modify(this);
            }
            victim.Health.CurrentValue -= damageTotal;
        }
    }
}

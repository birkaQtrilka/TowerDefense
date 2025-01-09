using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[RequireComponent(typeof(Bullet))]
public class BulletDamager : MonoBehaviour
{
    public Damage Damage;
    IDamageCondition[] _conditions;
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
        //get tower damage stat?

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

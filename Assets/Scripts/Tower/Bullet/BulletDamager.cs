using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletDamager : MonoBehaviour
{
    public int Damage;
    
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
        //get tower damage stat
        victim.GetHealth().CurrentValue -= Damage;
    }
}

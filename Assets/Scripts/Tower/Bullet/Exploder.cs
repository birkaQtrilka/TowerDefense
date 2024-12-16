using System;
using System.Collections;
using UnityEngine;
[RequireComponent (typeof(Projectile))]
public class Exploder : MonoBehaviour
{
    [SerializeField] float _explosionRadius;

    Projectile _projectile;
    Action<Enemy> OnHitEvent;
    private void Awake()
    {
        _projectile = GetComponent<Projectile>();
    }

    public void Explode()
    {
        _projectile.AddBeforeDeathAction(ExplosiveTimer(), out OnHitEvent);

    }

    IEnumerator ExplosiveTimer()
    {
        var collisions = Physics.OverlapSphere(transform.position,_explosionRadius);
        
        foreach (var collision in collisions)
        {
            var enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy == null) continue;
            Debug.Log("explosion effect");
            OnHitEvent(enemy);
        }

        OnHitEvent = null;
        yield return null;
    }
}

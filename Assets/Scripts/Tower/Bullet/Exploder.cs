using System;
using System.Collections;
using UnityEngine;
[RequireComponent (typeof(Projectile))]
public class Exploder : MonoBehaviour
{
    [SerializeField] float _explosionRadius;

    Projectile _projectile;
    Action<Enemy> _onHitEvent;
    bool added = false;

    void Awake()
    {
        _projectile = GetComponent<Projectile>();
    }

    public void Explode()
    {
        if (added) return;
        added = true;
        _projectile.AddBeforeDeathAction(ExplosiveTimer(), out _onHitEvent);

    }

    IEnumerator ExplosiveTimer()
    {
        var collisions = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (var collision in collisions)
        {
            var enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy == null) continue;
            Debug.Log("explosion effect");
            _onHitEvent(enemy);
        }

        _onHitEvent = null;
        yield return null;
    }
}

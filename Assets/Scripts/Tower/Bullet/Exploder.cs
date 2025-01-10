using System;
using System.Collections;
using UnityEngine;

//works only with projectiles
[RequireComponent (typeof(Projectile))]
public class Exploder : MonoBehaviour
{
    [SerializeField] float _explosionRadius;
    
    Projectile _projectile;
    Action<Enemy> _onHitCallback;

    //to prevent multiple explosions if the bullet hit more than one enemy
    bool _exploded = false;

    void Awake()
    {
        _projectile = GetComponent<Projectile>();
    }

    public void Explode()
    {
        if (_exploded) return;
        _exploded = true;
        _projectile.AddBeforeDeathAction(ExplosiveTimer(), out _onHitCallback);

    }
    //allows to wait for an animation or to have an explosion that's not instant
    IEnumerator ExplosiveTimer()
    {
        var collisions = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (var collision in collisions)
        {
            var enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy == null) continue;
            Debug.Log("explosion effect");
            _onHitCallback(enemy);
        }

        _onHitCallback = null;
        yield return null;
    }

    void OnDisable()
    {
        _onHitCallback = null;
    }
}

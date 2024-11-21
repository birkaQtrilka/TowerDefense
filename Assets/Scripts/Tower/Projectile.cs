using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class Projectile : Bullet
{
    public Vector3 Velocity { get => _rb.velocity; set => _rb.velocity = value; }
    Rigidbody _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out var enemy)) return;

        CallOnHitEvent(enemy.gameObject);
        //enemy get health
        Destroy(enemy.gameObject);
    }
}

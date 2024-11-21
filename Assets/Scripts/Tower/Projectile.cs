using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class Projectile : Bullet
{
    [field: SerializeField] public float StartSpeed {  get; set; }

    //public Vector3 Velocity { get => _rb.velocity; set => _rb.velocity = value; }
    Rigidbody _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _rb.velocity = transform.forward * StartSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out var enemy)) return;

        CallOnHitEvent(enemy.gameObject);
        //enemy get health
        Destroy(enemy.gameObject);
    }
}

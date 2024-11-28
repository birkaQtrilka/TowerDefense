using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class Projectile : Bullet
{
    [field: SerializeField] public float StartSpeed {  get; set; }

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

    public override void Init()
    {
        _rb.velocity = transform.forward * StartSpeed;
    }
}

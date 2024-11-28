using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class Projectile : Bullet
{
    [field: SerializeField] public float StartSpeed {  get; set; }
    [SerializeField] int _damage= 2;
    
    Rigidbody _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy == null) return;
        Debug.Log("Collided: " + collision.gameObject);

        CallOnHitEvent(enemy.gameObject);
        
        enemy.GetHealth().CurrentValue -= _damage;
    }

    public override void Init()
    {
        _rb.velocity = transform.forward * StartSpeed;
    }
}

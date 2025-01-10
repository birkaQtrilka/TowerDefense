using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet that is affected by gravity
/// </summary>
[RequireComponent (typeof(Rigidbody))]
public class Projectile : Bullet
{
    [field: SerializeField] public float StartSpeed {  get; set; }
    public Vector3 Velocity => _rb.velocity;

    [SerializeField] int _pierce = 1;
    int _currPierce;
    readonly Queue<IEnumerator> _beforeDeathQueue = new();
    //so it doesn't hit new enemies during an animation or other process
    bool _isDead;

    Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(_isDead) return;

        var enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy == null) return;
        CallOnHitEvent(enemy);
        OnEnemyCollide.Invoke(Sender, enemy);
        if(_currPierce-- <= 0)
            StartCoroutine(BeforeDeathQueue());
    }

    
    public void AddBeforeDeathAction(IEnumerator action, out Action<Enemy> OnHitEventCaller)
    {
        _beforeDeathQueue.Enqueue(action);
        //allows for other classes to call OnHit at the specific time of death
        OnHitEventCaller = CallOnHitEvent;
    }

    IEnumerator BeforeDeathQueue()
    {
        _isDead = true;
        while(_beforeDeathQueue.Count > 0)
        {
            IEnumerator currAction = _beforeDeathQueue.Dequeue();
            yield return currAction;
        }
        Destroy(gameObject);
    }

    public override void Init()
    {
        _rb.velocity = transform.forward * StartSpeed;
        _currPierce = _pierce;
    }

}

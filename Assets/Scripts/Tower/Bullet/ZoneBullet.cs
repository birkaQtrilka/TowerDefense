using UnityEngine;

[RequireComponent (typeof(Collider))]
public class ZoneBullet : Bullet
{
    SphereCollider _col;
    void Awake()
    {
        _col = GetComponent<SphereCollider>();
        _col.excludeLayers = ~LayerMask.GetMask("Enemy");
    }

    public override void Init()
    {
        _col.radius = Sender.Stats.GetStat<Range>().CurrentValue;
    }

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy == null) return;
        enabled = false;
        OnEnemyCollide.Invoke(Sender, enemy);
        CallOnHitEvent(enemy);
    }
}

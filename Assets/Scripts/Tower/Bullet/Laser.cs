using UnityEngine;
/// <summary>
/// HitScan bullet. raycast hit and drawn with a line renderer
/// </summary>
public class Laser : Bullet
{
    readonly RaycastHit[] _results = new RaycastHit[10];
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] LayerMask _canAttackMask;

    readonly Vector3[] _positions = new Vector3[2];

    public override void Init()
    {
        int count = Physics.RaycastNonAlloc(
            new Ray(transform.position, transform.forward), 
            _results, 
            Sender.Stats.GetStat<Range>().CurrentValue,
            _canAttackMask,
            QueryTriggerInteraction.Collide
        );
        if (count <= 0) return;
        Enemy enemy = null;
        for (int i = 0; i < count; i++)
        {
            enemy = _results[i].transform.GetComponentInParent<Enemy>();
        }
        if (enemy == null) return;
        
        OnEnemyCollide.Invoke(Sender, enemy);
        CallOnHitEvent(enemy);
        _positions[0] = transform.position;
        _positions[1] = enemy.transform.position;
        _lineRenderer.SetPositions(_positions);
    }

}

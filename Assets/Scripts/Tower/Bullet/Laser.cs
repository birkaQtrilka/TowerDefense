using UnityEngine;
using System.Linq;
public class Laser : Bullet
{
    readonly RaycastHit[] _results = new RaycastHit[10];
    [SerializeField] LineRenderer _lineRenderer;
    Vector3[] _positions = new Vector3[2];
    [SerializeField] LayerMask layers;

    public override void Init()
    {
        Debug.DrawRay(transform.position, transform.forward * Sender.Stats.GetStat<Range>().CurrentValue, Color.green, 1);
        //Debug.Break();
        int count = Physics.RaycastNonAlloc(
            new Ray(transform.position, transform.forward), 
            _results, 
            100,
            //Sender.Stats.GetStat<Range>().CurrentValue,
            layers,
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

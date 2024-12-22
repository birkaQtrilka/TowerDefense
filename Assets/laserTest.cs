using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserTest : MonoBehaviour
{
    [SerializeField] bool shoot;

    readonly RaycastHit[] _results = new RaycastHit[10];
    [SerializeField] LineRenderer _lineRenderer;
    Vector3[] _positions = new Vector3[2];
    [SerializeField] LayerMask layers;

    [SerializeField] Transform lookAt;
    void Update()
    {
        transform.LookAt(lookAt);

        if(shoot)
        {
            shoot = false;

            Debug.DrawRay(transform.position, transform.forward * 2, Color.blue, 1);
            //Debug.Break();
            int count = Physics.SphereCastNonAlloc(
                new Ray(transform.position, transform.forward),
                1f,
                _results,
                100,
                layers,
                QueryTriggerInteraction.Collide
            );
            Debug.Log(count);
            if (count <= 0) return;
            BuildManager enemy = null;
            for (int i = 0; i < count; i++)
            {

                enemy = _results[i].transform.GetComponentInParent<BuildManager>();
            }
            if (enemy == null) return;

            _positions[0] = transform.position;
            _positions[1] = enemy.transform.position;
            _lineRenderer.SetPositions(_positions);
        }
    }
}

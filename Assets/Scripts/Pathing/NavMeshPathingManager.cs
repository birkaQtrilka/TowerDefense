using System;
using UnityEngine;

public class NavMeshPathingManager : PathingManager
{
    [Header("It will shoot a ray in forward direction and set the hit as destination")]
    [SerializeField] GameObject _destinationRay;
    [SerializeField] GameObject _startRay;
    [SerializeField] LayerMask _layerMask;
    public override Vector3 GetDestination()
    {
        return ShootRay(_destinationRay);
    }

    public override Vector3 GetStartPosition()
    {
        return ShootRay(_startRay);

    }

    Vector3 ShootRay(GameObject go)
    {
        if (!Physics.Raycast(go.transform.position, go.transform.forward,
            out RaycastHit hit, 10,_layerMask.value))
        {
            Debug.LogError("could not hit any nav mesh collider");
            return Vector3.zero;
        }
        return hit.point;   
    }
}

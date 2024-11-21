using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class NavMeshWalker : MonoBehaviour, IWalker
{
    Vector3 _destination;
    NavMeshAgent _navAgent;

    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    public void WalkToDestination()
    {
        _navAgent.SetDestination(_destination);
    }
}

using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour, IMover
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

    public void MoveToDestination()
    {
        _navAgent.SetDestination(_destination);
    }
}

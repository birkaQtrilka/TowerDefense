using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour, IMover
{
    Vector3 _destination;
    NavMeshAgent _navAgent;

    public Vector3 Velocity => _navAgent.velocity;

    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    public void SetMoveSpeed(float speed)
    {
        if(_navAgent == null)
            _navAgent = GetComponent<NavMeshAgent>();

        _navAgent.speed = speed;
    }

    //to adapt to OnValueChanged from stat
    public void SetMoveSpeed(float previousVal, Stat<float> speed)
    {
        SetMoveSpeed(speed.CurrentValue);
    }

    public void MoveToDestination()
    {
        _navAgent.SetDestination(_destination);
    }
}

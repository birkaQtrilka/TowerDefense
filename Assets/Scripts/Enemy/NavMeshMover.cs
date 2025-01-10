using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Moves object according to a configured nav mesh and nav agent
/// </summary>
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
        //to prevent errors if the speed is set before SetActive is true
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

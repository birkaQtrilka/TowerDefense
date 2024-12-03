using UnityEngine;

public interface IMover 
{
    public Vector3 Velocity { get; }
    void SetDestination(Vector3 destination);
    void MoveToDestination();
}

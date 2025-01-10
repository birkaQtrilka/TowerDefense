using UnityEngine;
/// <summary>
/// Interface to move objects to a destination
/// </summary>
public interface IMover 
{
    public Vector3 Velocity { get; }
    void SetDestination(Vector3 destination);
    void MoveToDestination();
}

using UnityEngine;

public interface IMover 
{
    void SetDestination(Vector3 destination);
    void MoveToDestination();
}

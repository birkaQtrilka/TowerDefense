using UnityEngine;

public abstract class PathingManager : MonoBehaviour
{
    public abstract Vector3 GetDestination();   
    public abstract Vector3 GetStartPosition();
}

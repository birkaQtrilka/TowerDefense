using UnityEngine;
/// <summary>
/// Abstract class for deciding how the movement terrain is set
/// </summary>
public abstract class PathingManager : MonoBehaviour
{
    public abstract Vector3 GetDestination();   
    public abstract Vector3 GetStartPosition();
}

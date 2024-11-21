using UnityEngine;
[RequireComponent (typeof(IWalker))]
public class Enemy : MonoBehaviour
{
    IWalker _walker;
    
    void Awake()
    {
        _walker = GetComponent<IWalker>();
    }
    

}

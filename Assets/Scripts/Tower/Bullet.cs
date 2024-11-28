using System;
using UnityEngine;
//could also be called effect appllier
public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public GameObject Sender;

    //sender, victim
    public event Action<GameObject, GameObject> OnHit;
    protected void CallOnHitEvent(GameObject victim) => OnHit?.Invoke(Sender, victim);
    public abstract void Init();

    void OnDisable()
    {
        OnHit = null;
    }
}

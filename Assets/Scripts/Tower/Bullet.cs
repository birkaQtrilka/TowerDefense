using System;
using UnityEngine;
using UnityEngine.Events;
//could also be called effect appllier
public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public GameObject Sender;

    //sender, victim
    public UnityEvent<GameObject, GameObject> OnHit;
    protected void CallOnHitEvent(GameObject victim) => OnHit?.Invoke(Sender, victim);
    public abstract void Init();

    void OnDisable()
    {
        OnHit = null;
    }
}

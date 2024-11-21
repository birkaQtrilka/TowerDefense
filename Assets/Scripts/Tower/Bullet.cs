using System;
using UnityEngine;
//could also be called effect appllier
public abstract class Bullet : MonoBehaviour
{
    //sender, victim
    public event Action<GameObject, GameObject> OnHit;
    public GameObject Sender { get; set; }

    protected void CallOnHitEvent(GameObject victim) => OnHit?.Invoke(Sender, victim);
}

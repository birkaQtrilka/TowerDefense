using UnityEngine;
using UnityEngine.Events;
//could also be called effect appllier
public abstract class Bullet : MonoBehaviour
{
    public Tower Sender { get; set; }

   
    //this gets called only when the collision is validated
    public UnityEvent<Tower, Enemy> OnHit;
    //this gets called on every enemy collision
    public UnityEvent<Tower, Enemy> OnEnemyCollide;
    //legacy when the above events were c# events
    protected void CallOnHitEvent(Enemy victim) => OnHit?.Invoke(Sender, victim);
    public abstract void Init();

    void OnDisable()
    {
        OnHit = null;
    }

}

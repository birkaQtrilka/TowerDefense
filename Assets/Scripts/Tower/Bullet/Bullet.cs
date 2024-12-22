using UnityEngine;
using UnityEngine.Events;
//could also be called effect appllier
public abstract class Bullet : MonoBehaviour
{
    public Tower Sender { get; set; }

    //sender, victim
    public UnityEvent<Tower, Enemy> OnHit;
    public UnityEvent<Tower, Enemy> OnEnemyCollide;
    protected void CallOnHitEvent(Enemy victim) => OnHit?.Invoke(Sender, victim);
    public abstract void Init();

    void OnDisable()
    {
        OnHit = null;
    }

}

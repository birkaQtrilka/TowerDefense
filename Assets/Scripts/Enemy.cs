using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(IMover))]
public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    public UnityEvent<Enemy> OnDestroy;
    //[SerializeField] StatsContainer _stats;
    //IMover _walker;
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Speed Speed { get; private set; }
    [field: SerializeField] public CarriedMoney CarriedMoney { get; private set; }
    

    void OnEnable()
    {
        Health.CurrentUpdated.AddListener(CheckForDeath);    
        //for stuff to get initialized on start (move speed)
        Speed.CurrentValue = Speed.CurrentValue;
        
    }

    void OnDisable()
    {
        Health.CurrentUpdated.RemoveListener(CheckForDeath);
        OnDestroy?.Invoke(this);
    }
    

    void CheckForDeath(int prevHp, Stat<int> health)
    {
        if(health.CurrentValue == 0)
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }

}



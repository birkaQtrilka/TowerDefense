using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// The main enemy class, contains the main stats and can have extra ones in a container
/// </summary>
[RequireComponent(typeof(IMover))]
[SelectionBase]
public class Enemy : MonoBehaviour
{
    //the difference between those events is that sometimes the enemy is just destroyed but not killed by a tower
    public UnityEvent<Enemy> OnDeath;
    public UnityEvent<Enemy> OnDestroy;
    //IMover _walker;
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Speed Speed { get; private set; }
    [field: SerializeField] public CarriedMoney CarriedMoney { get; private set; }
    [SerializeField] StatsContainer _stats;
    

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



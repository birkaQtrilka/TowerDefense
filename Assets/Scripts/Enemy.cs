using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(IMover))]
public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    public UnityEvent<Enemy> OnDestroy;
    [SerializeField] StatsContainer _stats;
    //IMover _walker;

    Health _health;

    void Awake()
    {
        //_walker = GetComponent<IMover>();
        _health = _stats.GetStat<Health>();
    }

    void OnEnable()
    {
        _health.CurrentUpdated.AddListener(CheckForDeath);    
    }

    void OnDisable()
    {
        _health.CurrentUpdated.RemoveListener(CheckForDeath);
        OnDestroy?.Invoke(this);
    }

    public T GetStat<T>()where T : Stat => _stats.GetStat<T>(); 

    public Health GetHealth()
    {
        return _health;
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



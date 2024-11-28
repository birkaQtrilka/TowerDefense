using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(IMover))]
public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    [SerializeField] StatsContainer _stats;
    IMover _walker;

    Health _health;

    void Awake()
    {
        _walker = GetComponent<IMover>();
        _health = _stats.GetStat<Health>();
    }

    void OnEnable()
    {
        _health.OnCurrentUpdate.AddListener(CheckForDeath);    
    }

    void OnDisable()
    {
        _health.OnCurrentUpdate.RemoveListener(CheckForDeath);
    }

    public T GetStat<T>()where T : Stat => _stats.GetStat<T>(); 

    public Health GetHealth()
    {
        return _health;
    }

    void CheckForDeath(int prevHp,int hp)
    {
        if(hp == 0)
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }

}



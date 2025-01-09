using UnityEngine;

public class Debugging : MonoBehaviour
{
    public static Debugging Instance { get; private set; }

    [field:SerializeField] public bool HealthImmunity { get; private set; }
    [field:SerializeField] public bool InstaKill { get; private set; }
    [field:SerializeField] public bool InfiniteMoney { get; private set; }
    [field:SerializeField] public bool InstaGameOver { get; private set; }
    [field:SerializeField] public bool ToggleInfiniteLives { get; private set; }

    bool _toggled;

    bool _clickedInfinite;
    bool _clickedNotInfinite;

    LifeManager _lifeManagerCache;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        _lifeManagerCache = FindObjectOfType<LifeManager>();
        if(_lifeManagerCache != null)
        _toggled = _lifeManagerCache.gameObject.activeInHierarchy;
    }

    void Update()
    {

        if(_lifeManagerCache != null)
            HandleInfiniteLives();

        if(InstaGameOver && GameManager.Instance != null)
        {
            InstaGameOver = false;
            GameManager.Instance.TransitionToState(typeof(GameOver));
        }

        UpdateInfiniteMoney();
    }
    
    void HandleInfiniteLives()
    {
        if (ToggleInfiniteLives)
        {
            ToggleInfiniteLives = false;

            _toggled = !_toggled;

            _lifeManagerCache.gameObject.SetActive(_toggled);
        }

    }

    void UpdateInfiniteMoney()
    {
        if(InfiniteMoney)
        {
            _clickedNotInfinite = true;
            if(!_clickedInfinite)
            {
                _clickedInfinite = true;
                SetInfiniteStore();
            }

        }
        else
        {
            _clickedInfinite = false;
            if (!_clickedNotInfinite)
            {
                _clickedNotInfinite = true;
                SetInfiniteStore();
            }
        }
    }

    void SetInfiniteStore()
    {
        if (Store.Instance != null)
            Store.Instance.InfiniteMoney = InfiniteMoney;
    }
}

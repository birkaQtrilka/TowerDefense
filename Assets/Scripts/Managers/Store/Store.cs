using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Manages the Money and the buying validation
/// </summary>
public class Store : MonoBehaviour
{
    public static Store Instance { get; private set; }

    [field: SerializeField] public CarriedMoney Money { get; private set; }

    //it's outside the hierarchy of the store because elsewise it would react to screen res change weirdly
    [SerializeField] Button _startGameBtn;
    //to equip a tower on selection
    [SerializeField] TowerPlacer _towerPlacer;

    //for testing
    public bool InfiniteMoney { get; set; }

    //its for when the infinite is turned off, you have the old amount of money back
    int _moneyBeforeInfinite = -1;
    StoreSlot[] _slots;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

    }

    void OnEnable()
    {
        _slots = GetComponentsInChildren<StoreSlot>();

        Money.CurrentUpdated.AddListener(UpdateVisual);

        //subscribes to event in order to validate if you can buy an item
        EventBus<TryBuy>.Event += BuyResponse;

        foreach (var slot in _slots)
        {
            slot.Clicked += OnSlotPressed;
        }

        UpdateVisual(Money.CurrentValue, Money);
        _startGameBtn.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        Money.CurrentUpdated.RemoveListener(UpdateVisual);

        EventBus<TryBuy>.Event -= BuyResponse;
        TowerPlacer.TowerPlaced -= OnTowerPlaced;

        foreach (var slot in _slots)
        {
            slot.Clicked -= OnSlotPressed;
        }
        _startGameBtn.gameObject.SetActive(false);

    }


    void OnSlotPressed(TowerData data)
    {
        if (data.Price <= Money.CurrentValue)
        {
            TowerPlacer.TowerPlaced -= OnTowerPlaced;

            _towerPlacer.SelectTower(data);
            TowerPlacer.TowerPlaced += OnTowerPlaced;
        }
    }

    void OnTowerPlaced(TowerData towerData)
    {
        Money.CurrentValue -= towerData.Price;
        TowerPlacer.TowerPlaced -= OnTowerPlaced;
    }
    
    void BuyResponse(TryBuy tryBuy)
    {
        if (tryBuy.Amount <= Money.CurrentValue)
        {
            tryBuy.OnAllow();
            Money.CurrentValue -= tryBuy.Amount;

        }
    }

    void UpdateVisual(int oldVal, Stat<int> money)
    {
        if (!gameObject.activeInHierarchy) return;
        foreach (var slot in _slots)
        {
            slot.CanBeBought(slot.Data.Price <= money.CurrentValue); 
        }
    }

    public void AddMoney(int amount)
    {
        Money.CurrentValue += amount;
    }

    void Update()
    {
        //this if else hell is to make it so the Money is set for only one frame, to prevent calling the 
        //OnCurrentUpdate every frame and to prevent constant UI updates
        if(InfiniteMoney)
        {
            if(Money.CurrentValue != 999999)
            {
                if (_moneyBeforeInfinite == -1)
                    _moneyBeforeInfinite = Money.CurrentValue;
                Money.CurrentValue = 999999;
            }
        }
        else
        {
            if (_moneyBeforeInfinite != -1)
            {
                Money.CurrentValue = _moneyBeforeInfinite;
                _moneyBeforeInfinite = -1;
            }
        }
    }
}

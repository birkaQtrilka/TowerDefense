using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    StoreSlot[] _slots;
    [SerializeField] Button _startGameBtn;
    [field: SerializeField] public CarriedMoney Money { get; private set; }

    [SerializeField] TowerDataUI _ui;
    [SerializeField] TowerPlacer _towerPlacer;

    public static Store Instance { get; private set; }

    public bool InfiniteMoney { get; set; }

    int _moneyBeforeInfinite = -1;

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

        EventBus<TryBuy>.Event += BuyResponse;

        foreach (var slot in _slots)
        {
            slot.Clicked += OnSlotPressed;
            slot.Hover += OnSlotHover;
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
            slot.Hover -= OnSlotHover;
        }
        _startGameBtn.gameObject.SetActive(false);

    }

    void OnSlotHover(TowerData data)
    {
        _ui.UpdateVisual(data.Prefab, data);
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

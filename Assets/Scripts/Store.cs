using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    [SerializeField] List<StoreSlot> _slots;
    [SerializeField] Button _startGameBtn;
    [SerializeField] int _money = 50;
    [SerializeField] public int Money 
    {
        get
        {
            return _money;
        }
        private set 
        {
            _money = value;
            UpdateVisual();
        }
    }
    [SerializeField] TowerDataUI _ui;
    [SerializeField] TowerPlacer _towerPlacer;

    public static Store Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void OnEnable()
    {
        EventBus<TryBuy>.Event += BuyResponse;

        foreach (var slot in _slots)
        {
            slot.Clicked += OnSlotPressed;
            slot.Hover += OnSlotHover;
        }

        UpdateVisual();
        _startGameBtn.gameObject.SetActive(true);
    }

    void OnDisable()
    {
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
        if (data.Price <= Money)
        {
            TowerPlacer.TowerPlaced -= OnTowerPlaced;

            _towerPlacer.SelectTower(data);
            TowerPlacer.TowerPlaced += OnTowerPlaced;
        }
    }

    void OnTowerPlaced(TowerData towerData)
    {
        Money -= towerData.Price;
        TowerPlacer.TowerPlaced -= OnTowerPlaced;
        EventBus<MoneySpent>.Publish(new MoneySpent(towerData.Price, Money));
    }
    
    void BuyResponse(TryBuy tryBuy)
    {
        if (tryBuy.Amount <= Money)
        {
            tryBuy.OnAllow();
            Money -= tryBuy.Amount;
            EventBus<MoneySpent>.Publish(new MoneySpent(tryBuy.Amount, Money));

        }
    }

    public void Sell(TowerSeller seller)
    {
        Money -= seller.SellPrice;
    }

    public void UpdateVisual()
    {
        if (!gameObject.activeInHierarchy) return;
        foreach (var slot in _slots)
        {
            slot.CanBeBought(slot.Data.Price <= Money); 
        }
    }
}

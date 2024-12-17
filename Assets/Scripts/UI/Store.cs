using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    [SerializeField] List<StoreSlot> _slots;
    [field: SerializeField] public int Money { get; private set; }
    [SerializeField] TowerDataUI _ui;

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
            slot.Clicked += OnSlotSelect;
            slot.Hover += OnSlotHover;
        }

    }

    void OnDisable()
    {
        EventBus<TryBuy>.Event += BuyResponse;
        TowerPlacer.Instance.TowerPlaced -= OnTowerPlaced;

        foreach (var slot in _slots)
        {
            slot.Clicked -= OnSlotSelect;
            slot.Hover -= OnSlotHover;
        }
    }

    void OnSlotHover(TowerData data)
    {
        _ui.UpdateVisual(data.Prefab, data);
    }

    void OnSlotSelect(TowerData data)
    {
        if (data.Price <= Money)
        {
            TowerPlacer.Instance.SelectTower(data);
            TowerPlacer.Instance.TowerPlaced += OnTowerPlaced;
        }
    }

    void OnTowerPlaced(TowerData towerData)
    {
        Money -= towerData.Price;
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
}

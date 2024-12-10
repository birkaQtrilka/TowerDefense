using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    
    [SerializeField] List<StoreSlot> _slots;
    [SerializeField] public int Money;
    [SerializeField] TowerDataUI _ui;

    void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnTryBuy += OnTryBuy;
            slot.OnSelection += OnSelect;
        }

    }

    void OnDisable()
    {
        TowerPlacer.Instance.TowerPlaced -= OnTowerPlaced;

        foreach (var slot in _slots)
        {
            slot.OnTryBuy -= OnTryBuy;
            slot.OnSelection -= OnSelect;
        }
    }

    void OnSelect(TowerData data)
    {
        Debug.Log("Hover");
        
        _ui.UpdateVisual(data.Prefab, data);
    }

    void OnTryBuy(TowerData data)
    {
        Debug.Log("clicked");

        if (data.Price <= Money)
        {
            Debug.Log("can buy");

            TowerPlacer.Instance.SelectTower(data);
            TowerPlacer.Instance.TowerPlaced += OnTowerPlaced;
        }
    }



    void OnTowerPlaced(TowerData towerData)
    {
        Money -= towerData.Price;
        EventBus<MoneySpent>.Publish(new MoneySpent(towerData.Price, Money));
    }

}

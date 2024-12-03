using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    
    [SerializeField] List<StoreSlot> _slots;
    [SerializeField] 
    public int Money;

    void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnTryBuy += OnTryBuy;
        }
    }
    void OnDisable()
    {
        foreach (var slot in _slots)
        {
            slot.OnTryBuy -= OnTryBuy;
        }
    }

    void OnTryBuy(TowerData data)
    {
        if(data.Price <= Money)
        {
            Money -= data.Price;
            TowerPlacer.Instance.SelectTower(data);
        }
    }

    void OnSelection()
    {

    }
}

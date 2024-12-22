using System;
using UnityEngine;

public class TowerSeller : MonoBehaviour
{
    public static event Action<Tower> Sold;
    [field: SerializeField] public int SellPrice { get; set; }

    Tower _tower;

    void Awake()
    {
        _tower = GetComponent<Tower>();
    }

    public void Sell()
    {
        //CAN DO: 
        //ADD CanSell()
        Store.Instance.AddMoney(SellPrice);
        Sold.Invoke(_tower);
        Destroy(gameObject);
    }
}

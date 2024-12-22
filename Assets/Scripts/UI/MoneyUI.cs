using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text _txtMesh;
    
    void OnEnable()
    {
        StartCoroutine(WhenAllocated(() =>
        {
            Store.Instance.Money.CurrentUpdated.AddListener(OnMoneySpent);
            UpdateVisual(Store.Instance.Money.CurrentValue);
        }));
    }

    IEnumerator WhenAllocated(Action action)
    {
        var condition = new WaitUntil(() => Store.Instance != null);
        yield return condition;
        action();
    }

    void OnDisable()
    {
        if(Store.Instance != null) 
            Store.Instance.Money.CurrentUpdated.RemoveListener(OnMoneySpent);

    }

    void OnMoneySpent(int old, Stat<int> money)
    {
        UpdateVisual(money.CurrentValue);
    }

    void UpdateVisual(int val)
    {
        _txtMesh.text = val.ToString();
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text _txtMesh;

    void OnEnable()
    {
        EventBus<MoneySpent>.Event += OnMoneySpent;
        StartCoroutine(WhenAllocated(() => UpdateVisual(Store.Instance.Money)));
    }

    IEnumerator WhenAllocated(Action action)
    {
        var condition = new WaitUntil(() => Store.Instance != null);
        yield return condition;
        action();
    }

    void OnDisable()
    {
        EventBus<MoneySpent>.Event -= OnMoneySpent;

    }

    void OnMoneySpent(MoneySpent moneySpent)
    {
        UpdateVisual(moneySpent.NewTotal);
    }

    void UpdateVisual(int val)
    {
        _txtMesh.text = val.ToString();
    }
}

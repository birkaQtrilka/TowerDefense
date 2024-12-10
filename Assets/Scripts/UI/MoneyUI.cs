using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text _txtMesh;

    void OnEnable()
    {
        EventBus<MoneySpent>.Event += OnMoneySpent;
    }

    void OnDisable()
    {
        EventBus<MoneySpent>.Event -= OnMoneySpent;

    }

    void OnMoneySpent(MoneySpent moneySpent)
    {
        _txtMesh.text = moneySpent.NewTotal.ToString();

    }
}

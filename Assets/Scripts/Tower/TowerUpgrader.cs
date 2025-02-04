using System;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerUpgrader : MonoBehaviour
{
    public static event Action<TowerUpgrader, TowerUpgrader> Upgraded;

    //upgrade data
    [field: SerializeField] public Sprite UiImage { get; private set; }
    [field: SerializeField] public GameObject CanUpgradeVisual { get; private set; }

    [field: SerializeField] public int UpgradeCost { get; private set; }

    public TowerData Data { get; private set; }
    public Tower Tower => _instance;

    int _currentUpdate;
    Tower _instance;
    //to prevent setting the same value every frame
    bool _lastCanShowUpgradeValue;

    public void Init(TowerData data, int currentIndex)
    {
        Data = data;
        _currentUpdate = currentIndex;
        _instance = GetComponent<Tower>();

        CanUpgradeVisual = Instantiate(CanUpgradeVisual, transform);
        _lastCanShowUpgradeValue = CheckCanShowUpdate();
        CanUpgradeVisual.SetActive(_lastCanShowUpgradeValue);

    }

    public void DoUpgrade()
    {
        if (_currentUpdate >= Data.UpgradeList.Length - 1) return;
        //the upgrade method will be called if the listener to this event allows it
        EventBus<TryBuy>.Publish(new TryBuy(UpgradeCost, Upgrade));
    }

    void Upgrade()
    {
        var upgradedInstance = Instantiate(Data.UpgradeList[++_currentUpdate]);

        var nextUpgrader = upgradedInstance.GetComponent<TowerUpgrader>();
        if (nextUpgrader != null)
            nextUpgrader.Init(Data, _currentUpdate);
        Upgraded?.Invoke(this, nextUpgrader);
        Destroy(_instance.gameObject);
    }


    bool CheckCanShowUpdate()
    {
        return Data != null &&
            _currentUpdate < Data.UpgradeList.Length - 1 &&
            Store.Instance != null &&
            UpgradeCost <= Store.Instance.Money.CurrentValue &&
            GameManager.Instance != null &&
            GameManager.Instance.CurrentState is Build;
    }

    void FixedUpdate()//I could've also reacted to store OnCurrentValue update
    {
        bool canShowUpgrade = CheckCanShowUpdate();

        if (canShowUpgrade != _lastCanShowUpgradeValue)
        {
            CanUpgradeVisual.SetActive(canShowUpgrade);
            _lastCanShowUpgradeValue = canShowUpgrade;
        }
    }
}

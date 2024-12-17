using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Tower))]
public class TowerUpgrader : MonoBehaviour
{
    //previous upgrade, current upgrade
    //[field: SerializeField] public UnityEvent<TowerUpgrader, TowerUpgrader> Upgraded { get; private set; }

    //upgrade data
    [field: SerializeField] public Sprite UiImage {  get; private set; }
    [field: SerializeField] public int UpgradeCost {  get; private set; }

    public TowerData Data => _data;
    public Tower Tower => _instance;

    TowerData _data;
    int _currentUpdate;
    Tower _instance;

    public void Init(TowerData data, int currentIndex)
    {
        _data = data;
        _currentUpdate = currentIndex;
        _instance = GetComponent<Tower>();
    }

    public void DoUpgrade()
    {
        if (_currentUpdate >= _data.UpgradeList.Length - 1) return;
        EventBus<TryBuy>.Publish(new TryBuy(UpgradeCost, Upgrade));
    }

    void Upgrade()
    {
        var upgradedInstance = Instantiate(_data.UpgradeList[++_currentUpdate]);

        var nextUpgrader = upgradedInstance.GetComponent<TowerUpgrader>();

        nextUpgrader.Init(_data, _currentUpdate);

        EventBus<TowerUpgraded>.Publish(new TowerUpgraded(this, nextUpgrader));
        Destroy(_instance.gameObject);
    }
}

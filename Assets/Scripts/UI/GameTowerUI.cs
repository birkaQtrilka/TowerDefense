using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

public class GameTowerUI : MonoBehaviour
{
    [SerializeField] Image _selectionImg;
    [SerializeField] Text _fireSpeedText;
    [SerializeField] Text _rangeText;
    [SerializeField] Text _damageText;
    [SerializeField] Text _damageTypeText;
    [SerializeField] Text _nameText;
    [SerializeField] Text _priceText;
    [SerializeField] Button _upgradeBtn;

    TowerUpgrader _currUpgrader;

    //on upgrade, the object gets destroyed, but I'm still looking at the the same upgrade. Needs to update on upgrade?
    public void UpdateVisual(Tower tower)
    {
        TowerUpgrader upgrader = tower.GetComponent<TowerUpgrader>();

        UpdateVisual(upgrader);
    }

    public void UpdateVisual(TowerUpgrader upgrader)
    {
        Tower tower = upgrader.Tower;

        _selectionImg.sprite = upgrader.UiImage;

        _nameText.text = upgrader.Data.Name;

        _fireSpeedText.text = tower.Stats.GetStat<Speed>().OriginalValue.ToString();
        Damage towerDamage = tower.BulletPrefab.GetComponent<BulletDamager>().Damage;
        _damageText.text = towerDamage.OriginalValue.ToString();
        _damageTypeText.text = towerDamage.Type.ToString();
        _rangeText.text = tower.Stats.GetStat<Range>().ToString();
        _priceText.text = upgrader.UpgradeCost.ToString();

        _currUpgrader = upgrader;

        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(UpgradeTower);
    }

    public void UpgradeTower()
    {
        _currUpgrader.DoUpgrade();
    }

    void OnUpgrade(TowerUpgraded evnt)
    {
        if(_currUpgrader == evnt.OldUpgrader)
        {
            _currUpgrader = evnt.CurrentUpgrader;
            UpdateVisual(_currUpgrader);
        }
    }

    void OnEnable()
    {
        EventBus<TowerUpgraded>.Event += OnUpgrade;
    }

    void OnDisable()
    {
        EventBus<TowerUpgraded>.Event -= OnUpgrade;
        _upgradeBtn.onClick.RemoveAllListeners();

    }

}

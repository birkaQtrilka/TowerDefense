using UnityEngine;
using UnityEngine.UI;

public class TowerDataUI : MonoBehaviour
{
    [SerializeField] Image _selectionImg;
    [SerializeField] Text _fireSpeedText;
    [SerializeField] Text _descriptionText;
    [SerializeField] Text _rangeText;
    [SerializeField] Text _damageText;
    [SerializeField] Text _damageTypeText;
    [SerializeField] Text _nameText;
    
    public void UpdateVisual(Tower tower, TowerData data)
    {
        _selectionImg.sprite = data.SelectionImage;

        _nameText.text = data.Name;

        _fireSpeedText.text = tower.Stats.GetStat<Speed>().OriginalValue.ToString();
        Damage towerDamage = tower.BulletPrefab.GetComponent<BulletDamager>().Damage;
        _damageText.text = towerDamage.OriginalValue.ToString();
        _damageTypeText.text = towerDamage.Type.ToString();
        _rangeText.text = tower.GetComponent<TargetFinder>().Range.ToString();
        _descriptionText.text = data.Description;
    }
}

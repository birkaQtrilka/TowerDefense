using UnityEngine;

//strategy of displaying the tower data on a UI panel
[RequireComponent (typeof(Tower))]
public class TowerUISelection : SelectionVisual
{
    GameTowerUI _gameTowerUI;
    Tower _tower;

    void Awake()
    {
        _gameTowerUI = FindObjectOfType<GameTowerUI>(true);
        _tower = GetComponent<Tower>();
    }

    public override void Deselect()
    {
        _gameTowerUI.gameObject.SetActive(false);
    }

    public override void Select()
    {
        _gameTowerUI.gameObject.SetActive(true);
        _gameTowerUI.UpdateVisual(_tower);
    }

}

using UnityEngine;

/// <summary>
/// Responsible for placing the tower in a correct way. Handles when the tower is sold or swapped
/// </summary>
public class TowerPlaceSpot : MonoBehaviour
{
    [SerializeField] GameObject _hoverVisual;
    [SerializeField] Transform _placePos;

    public Tower HoldingTower { get; private set; }


    void OnEnable()
    {
        TowerUpgrader.Upgraded += OnTowerUpgraded;
    }

    void OnDisable()
    {
        TowerUpgrader.Upgraded -= OnTowerUpgraded;

    }
    
    //since the event is static, we need to first check if our tower is upgraded
    void OnTowerUpgraded(TowerUpgrader old, TowerUpgrader current)
    {
        if (old.Tower == HoldingTower)
        {
            PlaceTower(current.Tower);
        }
    }

    public void PlaceTower(Tower tower)
    {
        HoldingTower = tower;
        HoldingTower.transform.position = _placePos.position;
    }    

    public void HoverState()
    {
        _hoverVisual.SetActive(true);
    }

    public void StopHoverState()
    {
        _hoverVisual.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;

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

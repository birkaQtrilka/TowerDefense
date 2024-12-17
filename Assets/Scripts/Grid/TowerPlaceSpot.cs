using System.Collections;
using UnityEngine;

public class TowerPlaceSpot : MonoBehaviour
{
    [SerializeField] GameObject _hoverVisual;
    [SerializeField] Transform _placePos;

    public Tower HoldingTower { get; private set; }


    void OnEnable()
    {
        EventBus<TowerUpgraded>.Event += OnTowerUpgraded;
    }

    void OnTowerUpgraded(TowerUpgraded evnt)
    {
        if(evnt.OldUpgrader.Tower == HoldingTower)
        {
            PlaceTower(evnt.CurrentUpgrader.Tower);
        }
    }

    void OnDisable()
    {
        EventBus<TowerUpgraded>.Event -= OnTowerUpgraded;

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

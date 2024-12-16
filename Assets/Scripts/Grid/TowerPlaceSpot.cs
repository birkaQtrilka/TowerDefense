using System.Collections;
using UnityEngine;

public class TowerPlaceSpot : MonoBehaviour
{
    [SerializeField] GameObject _hoverVisual;
    [SerializeField] Transform _placePos;

    public Tower HoldingTower { get; private set; }

    public void PlaceTower(TowerData data)
    {
        HoldingTower = Instantiate(data.Prefab);
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

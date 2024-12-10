using System.Collections;
using UnityEngine;

public class TowerPlaceSpot : MonoBehaviour
{
    [SerializeField] GameObject _hoverVisual;
    [SerializeField] Transform _placePos;

    public void PlaceTower(TowerData data)
    {
        Tower tower = Instantiate(data.Prefab);
        tower.transform.position = _placePos.position;
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

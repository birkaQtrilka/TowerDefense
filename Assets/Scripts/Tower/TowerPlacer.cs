using System;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public static event Action<TowerData> TowerPlaced;

    [SerializeField] LayerMask _tileMask;

    TowerData _currentSelection;
    TowerPlaceSpot _prevPlace;
    Camera _camera;

    public void SelectTower(TowerData data)
    {
        _currentSelection = data;
    }

    public void Deselect()
    {
        TowerPlaced = null;
        _currentSelection = null;
    }

    void OnEnable()
    {
        _camera = Camera.main;
    }

    void OnDisable()
    {
        Deselect();
        DeselectHover();   
    }

    void DeselectHover()
    {
        if (_prevPlace != null)
        {
            _prevPlace.StopHoverState();
            _prevPlace = null;
        }
    }

    void Update()
    {
        DeselectHover();

        bool clicked = Input.GetMouseButtonDown(0);


        if (_currentSelection == null || 
            !Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 999, _tileMask) || 
            !hit.transform.TryGetComponent(out TowerPlaceSpot selection)
           )
        {
            if(clicked)
                Deselect();
            return; 
        }

        selection.HoverState();
        _prevPlace = selection;

        if (!clicked || selection.HoldingTower != null) return;
        
        Tower inst = Instantiate(_currentSelection.Prefab);
        inst.GetComponent<TowerUpgrader>().Init(_currentSelection, currentIndex: 0);
        selection.PlaceTower(inst);

        TowerPlaced?.Invoke(_currentSelection);
        Deselect();
    }

}

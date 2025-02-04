using System;
using UnityEngine;
using UnityEngine.Events;

public class TowerPlacer : MonoBehaviour
{
    public static event Action<TowerData> TowerPlaced;
    [field: SerializeField] public UnityEvent<TowerData> TowerSelected { get; private set; }
    [field: SerializeField]public UnityEvent<TowerData> TowerDeselected { get; private set; }

    [SerializeField] LayerMask _tileMask;

    TowerData _currentSelection;
    //it's for deselecting previous tiles
    TowerPlaceSpot _prevPlace;
    Camera _camera;

    public void SelectTower(TowerData data)
    {
        _currentSelection = data;
        TowerSelected?.Invoke(data);
    }

    public void Deselect()
    {
        TowerDeselected?.Invoke(_currentSelection);

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
            if(clicked)  Deselect();
            return; 
        }

        selection.HoverState();
        _prevPlace = selection;

        if (!clicked || selection.HoldingTower != null) return;

        PlaceSelectedTower(selection);

        Deselect();
    }


    void PlaceSelectedTower(TowerPlaceSpot placeSpot)
    {
        Tower inst = Instantiate(_currentSelection.Prefab);
        inst.GetComponent<TowerUpgrader>().Init(_currentSelection, currentIndex: 0);
        placeSpot.PlaceTower(inst);

        TowerPlaced?.Invoke(_currentSelection);
    }
}

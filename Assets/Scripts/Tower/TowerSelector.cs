using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] float _sphereCastRadius;
    [SerializeField] LayerMask _selectionLayer;
    Camera _camera;

    SelectionVisual _currentSelection;
    List<RaycastResult> _hits = new List<RaycastResult>();

    void Start()
    {
        _camera = Camera.main;    
    }

    void OnEnable()
    {
        TowerUpgrader.Upgraded += OnUpgrade;
        TowerSeller.Sold += OnSell;
    }

    void OnDisable()
    {
        TowerUpgrader.Upgraded -= OnUpgrade;
        TowerSeller.Sold -= OnSell;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TrySelectObject();

    }

    void TrySelectObject()
    {
        PointerEventData pointerEventData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        _hits.Clear();
        EventSystem.current.RaycastAll(pointerEventData, _hits);

        if (_hits.Count != 0) return;

        if (!Physics.SphereCast(_camera.ScreenPointToRay(Input.mousePosition), _sphereCastRadius,
            out var hit, 999, _selectionLayer.value))
        {
            Deselect();
            return;
        }
        SelectionVisual hitSelection = hit.transform.GetComponentInParent<SelectionVisual>();

        if (hitSelection == null) return;

        if (hitSelection == _currentSelection)
        {
            Deselect();
        }
        else
        {
            Deselect();
            Select(hitSelection);
        }
    }
    //same issue as before, upgrader destroys tower, so I can't deselect
    void Deselect()
    {
        if (_currentSelection != null)
        {
            _currentSelection.Deselect();
        }
        _currentSelection = null;
    }

    void Select(SelectionVisual visual)
    {
        _currentSelection = visual;
        _currentSelection.Select();
    }

    void OnUpgrade(TowerUpgrader old, TowerUpgrader current)
    {
        SelectionVisual oldVisual = old.GetComponent<SelectionVisual>();
        if (oldVisual == _currentSelection)
        {
            Deselect();
            Select(current.GetComponent<SelectionVisual>());
        }
    }

    void OnSell(Tower tower)
    {
        SelectionVisual selection = tower.GetComponent<SelectionVisual>();
        if (selection == _currentSelection)
        {
            Deselect();
        }
    }
}

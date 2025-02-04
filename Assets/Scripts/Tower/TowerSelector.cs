using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//checks for tower hits and activates a data displayer
public class TowerSelector : MonoBehaviour
{
    //to make it easier to hit towers
    [SerializeField] float _sphereCastRadius;
    [SerializeField] LayerMask _selectionLayer;
    Camera _camera;

    SelectionVisual _currentSelection;
    readonly List<RaycastResult> _hits = new();

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

    public void Deselect()
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

    //since upgrading works by destroying old version and spawning a new one, the selection must react
    void OnUpgrade(TowerUpgrader old, TowerUpgrader current)
    {
        SelectionVisual oldVisual = old.GetComponent<SelectionVisual>();
        //the event is static so I must check if the upgraded tower is also the selected one
        if (oldVisual == _currentSelection)
        {
            Deselect();
            Select(current.GetComponent<SelectionVisual>());
        }
    }

    void OnSell(Tower tower)
    {
        SelectionVisual selection = tower.GetComponent<SelectionVisual>();
        //the event is static so I must check if the sold tower is also the selected one
        if (selection == _currentSelection)
        {
            Deselect();
        }
    }
}

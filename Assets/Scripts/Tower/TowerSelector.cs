using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] float _sphereCastRadius;
    [SerializeField] LayerMask _selectionLayer;
    Camera _camera;

    SelectionVisual _currentSelection;

    void Start()
    {
        _camera = Camera.main;    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TrySelectObject();
    }

    void TrySelectObject()
    {
        if (!Physics.SphereCast(_camera.ScreenPointToRay(Input.mousePosition),
                _sphereCastRadius, out RaycastHit hit, 999, _selectionLayer.value))
        {
            Deselect();
            return;
        }
        var hitSelection = hit.transform.GetComponentInParent<SelectionVisual>();

        if (hitSelection == _currentSelection)
        {
            Deselect();
        }
        else
        {
            Deselect();
            _currentSelection = hitSelection;
            _currentSelection.Select();
        }
    }

    void Deselect()
    {
        if (_currentSelection != null)
            _currentSelection.Deselect();
        _currentSelection = null;
    }
}

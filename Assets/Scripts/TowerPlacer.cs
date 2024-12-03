using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance {  get; private set; }
    
    TowerData _currentSelection;

    public void SelectTower(TowerData data)
    {
        _currentSelection = data;
    }

    public void Deselect(TowerData data)
    {
        _currentSelection = null;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

}

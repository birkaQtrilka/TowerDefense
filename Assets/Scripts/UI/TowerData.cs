using UnityEngine;

public class TowerData : ScriptableObject
{
    [field: SerializeField] public string Name{ get; private set; }
    [field: SerializeField] public Tower Prefab { get; private set; }
    [field: SerializeField] public int Price {  get; private set; }
    [field: SerializeField] public Sprite StoreSlotImage { get; private set; }
    [field: SerializeField] public Sprite SelectionImage { get; private set; }
    [field: SerializeField] public string Description{ get; private set; }
}

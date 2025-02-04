using UnityEngine;

[CreateAssetMenu(menuName ="Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [field: SerializeField] public string Name{ get; private set; }
    [field: SerializeField] public int Price {  get; private set; }
    [field: SerializeField] public Sprite StoreSlotImage { get; private set; }
    [field: SerializeField] public Sprite SelectionImage { get; private set; }
    [field: SerializeField, TextArea] public string Description{ get; private set; }
    [field: SerializeField] public Tower[] UpgradeList { get; private set; }
    public Tower Prefab => UpgradeList[0];


}

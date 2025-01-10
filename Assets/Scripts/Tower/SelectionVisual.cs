using UnityEngine;

/// <summary>
/// Used for visualising the data of an item
/// </summary>
public abstract class SelectionVisual : MonoBehaviour
{
    public abstract void Select();
    public abstract void Deselect();
}

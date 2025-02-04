using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacerUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _defaultImage;

    public void OnTowerSelect(TowerData data)
    {
        _image.sprite = data.SelectionImage;
    }

    public void OnTowerDeselect(TowerData data)
    {
        _image.sprite = _defaultImage;

    }
}

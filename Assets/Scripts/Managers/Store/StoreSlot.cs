using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI of each item in store
/// </summary>
[RequireComponent (typeof(Button))]
public class StoreSlot : MonoBehaviour
{
    public event Action<TowerData> Clicked;
    public event Action<TowerData> Hover;

    [SerializeField] TowerData _data;
    [SerializeField] Image _image;
    [SerializeField] Image _notEnoughMoneyImg;
    Button _button;

    public TowerData Data
    { 
        get { return _data; } 
        set 
        { 
            _data = value;
            UpdateSlot();
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (Data == null)
        {
            return;
        }
        if (_image.sprite != _data)    
            UpdateSlot();
    }
#endif

    void UpdateSlot()
    {
        _image.sprite = _data.StoreSlotImage;
    }

    public void CanBeBought(bool canBe)
    {
        _notEnoughMoneyImg.enabled = !canBe;
    }

    void OnEnable()
    {
         if(_button==null)
            _button = GetComponent<Button>();

        _button.onClick.AddListener(OnBtnClick);
        UpdateSlot();
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(OnBtnClick);

    }

    void OnBtnClick()
    {
        Clicked?.Invoke(_data);
    }

    void OnMouseEnter()
    {
        Hover?.Invoke(_data);
    }
}

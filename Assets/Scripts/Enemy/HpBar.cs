using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// A world overlay canvas way of representing hp
/// </summary>
public class HpBar : MonoBehaviour
{
    [SerializeField] Image _fill;

    public void UpdateBar(int oldVal, Stat<int> health)
    {
        _fill.fillAmount = health.CurrentValue / (float)health.MaxValue;
    }

}

using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Image _fill;

    public void UpdateBar(int oldVal, Stat<int> health)
    {
        _fill.fillAmount = health.CurrentValue / (float)health.MaxValue;
    }

}

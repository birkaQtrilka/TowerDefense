using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Image _fill;

    [SerializeField] Enemy _enemy;
    Health _health;

    void OnEnable()
    {
        _health = _enemy.GetHealth();
        _health.OnCurrentUpdate.AddListener(UpdateBar);
        _health.OnMaxUpdate.AddListener(UpdateBar);

        //UpdateBar(0,0);
    }

    void UpdateBar(int oldVal, int newVal)
    {
        _fill.fillAmount = _health.CurrentValue / (float)_health.MaxValue;
    }

    void OnDisable()
    {
        _health.OnCurrentUpdate.RemoveListener(UpdateBar);
        _health.OnMaxUpdate.RemoveListener(UpdateBar);

    }

}

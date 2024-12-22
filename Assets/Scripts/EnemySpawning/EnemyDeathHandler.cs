using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyDeathHandler : MonoBehaviour
{
    Enemy _enemy;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        _enemy.OnDeath.AddListener(OnDeath);
    }

    void OnDisable()
    {
        _enemy.OnDeath.RemoveListener(OnDeath);
    }

    void OnDeath(Enemy enemy)
    {
        Store.Instance.AddMoney(enemy.CarriedMoney.CurrentValue);
    }
}

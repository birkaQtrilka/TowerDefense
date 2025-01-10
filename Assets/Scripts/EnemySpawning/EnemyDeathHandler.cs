using UnityEngine;

//just a class that is responsible for handling death, right now it's pretty empty
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
        if (Store.Instance != null)
            Store.Instance.AddMoney(enemy.CarriedMoney.CurrentValue);
    }
}

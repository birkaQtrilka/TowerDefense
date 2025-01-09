using UnityEngine;

public class Slower : MonoBehaviour
{
    public float SlowAmount;

    Bullet _bullet;
    void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    void OnEnable()
    {
        _bullet.OnHit.AddListener(DoSlow);
    }

    void OnDisable()
    {
        if(_bullet != null)
        _bullet.OnHit?.RemoveListener(DoSlow);
    }

    public void DoSlow(Tower sender, Enemy victim)
    {
        //get tower damage stat
        var enemySpeed = victim.Speed;

        float slowVal = enemySpeed.OriginalValue - SlowAmount;
        if (slowVal < enemySpeed.CurrentValue)
            enemySpeed.CurrentValue = slowVal;
    }
}

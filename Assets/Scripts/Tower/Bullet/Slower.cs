using UnityEngine;

public class Slower : MonoBehaviour
{
    public float SlowAmount;
    public const float MAX_SLOWNESS = 0.1f;
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
        //it looks at the original value, so the slow effect doesn't stack
        float slowVal = enemySpeed.OriginalValue - SlowAmount;
        if (slowVal < enemySpeed.CurrentValue && slowVal > MAX_SLOWNESS)
            enemySpeed.CurrentValue = slowVal;

    }
}

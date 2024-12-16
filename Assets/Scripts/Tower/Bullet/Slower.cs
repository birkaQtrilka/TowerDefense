using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : MonoBehaviour
{
    public float _slowAmount;

    Bullet _bullet;
    void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    void OnEnable()
    {
        //On hit automatically remvoes all listeners, no need for onDisable decoupling
        _bullet.OnHit.AddListener(DoSlow);
    }

    public void DoSlow(Tower sender, Enemy victim)
    {
        //get tower damage stat
        var enemySpeed = victim.GetStat<Speed>();

        float slowVal = enemySpeed.OriginalValue - _slowAmount;
        if ( slowVal < enemySpeed.CurrentValue)
            enemySpeed.CurrentValue = slowVal;
    }
}

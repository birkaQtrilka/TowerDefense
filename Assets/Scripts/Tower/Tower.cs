using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Tower : MonoBehaviour//make it generic and accept scriptable objects?
{
    [field: SerializeField] public StatsContainer Stats { get; private set; }

    [SerializeField] Bullet _bulletPrefab;


    [SerializeField] TargetFinder _finder;
    [SerializeField] Aimer _aimer;
    [SerializeField] Transform _bulletSpawnSpot;

    Speed _attackSpeed;
    float _currSpeed;

    void OnDrawGizmos()
    {
        
        Gizmos.DrawRay(transform.position, _aimer.GetAttackLook(null, _finder.GetAvailableTargets()) * Vector3.forward);    
    }
    
    void Awake()
    {
         _attackSpeed = Stats.GetStat<Speed>();
    }
    
    void Update()
    {
        if (_finder.GetSingleTarget() == null) return;
        //put this in view class?
        transform.rotation = _aimer.GetAttackLook(null, _finder.GetAvailableTargets());
        _currSpeed += Time.deltaTime;
        if (_currSpeed < _attackSpeed.CurrentValue) return;
        _currSpeed = 0;
        Shoot();
    }

    //this will be abstract?
    void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        
        bullet.transform.SetPositionAndRotation(
            position: _bulletSpawnSpot.position,
            rotation: _aimer.GetAttackLook(bullet, _finder.GetAvailableTargets())
        );

        bullet.Sender = gameObject;
        bullet.Init();
    }

}

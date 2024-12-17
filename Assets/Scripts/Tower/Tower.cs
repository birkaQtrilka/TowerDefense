using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Tower : MonoBehaviour//make it generic and accept scriptable objects?
{
    [field: SerializeField] public StatsContainer Stats { get; private set; }

    [field: SerializeField] public Bullet BulletPrefab {  get; private set; }


    [SerializeField] TargetFinder _finder;
    [SerializeField] Aimer _aimer;
    [SerializeField] Transform _bulletSpawnSpot;

    Speed _attackCooldown;
    float _currCooldown;


    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, _aimer.GetAttackLook(null, _finder.GetAvailableTargets()) * Vector3.forward);    
    }
    
    void Awake()
    {
         _attackCooldown = Stats.GetStat<Speed>();
    }
    
    void Update()
    {
        if (_finder.GetSingleTarget() == null) return;
        //put this in view class?
        transform.rotation = _aimer.GetAttackLook(null, _finder.GetAvailableTargets());
        _currCooldown += Time.deltaTime;
        if (_currCooldown < _attackCooldown.CurrentValue) return;
        _currCooldown = 0;
        Shoot();
    }

    //this will be abstract?
    void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab);
        
        bullet.transform.SetPositionAndRotation(
            position: _bulletSpawnSpot.position,
            rotation: _aimer.GetAttackLook(bullet, _finder.GetAvailableTargets())
        );

        bullet.Sender = this;
        bullet.Init();
    }

}

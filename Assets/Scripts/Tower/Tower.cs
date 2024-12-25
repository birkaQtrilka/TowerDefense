using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Tower : MonoBehaviour//make it generic and accept scriptable objects?
{
    [SerializeField] UnityEvent _onTowerShoot;

    [field: SerializeField] public StatsContainer Stats { get; private set; }

    [field: SerializeField] public Bullet BulletPrefab {  get; private set; }


    [SerializeField] TargetFinder _finder;
    [SerializeField] Aimer _aimer;
    [SerializeField] Transform _bulletSpawnSpot;
    [SerializeField] Transform _yRotator;
    [SerializeField] Transform _xRotator;

    Speed _attackCooldown;
    float _currCooldown;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if(_aimer != null && _finder != null)
        Gizmos.DrawRay(_bulletSpawnSpot.position, _aimer.GetAttackLook(BulletPrefab, _finder.GetAvailableTargets()) * Vector3.forward * 3);    
    }
    
    void Awake()
    {
         _attackCooldown = Stats.GetStat<Speed>();
        if (_aimer == null) return;

        if(_xRotator != null)
            _aimer.DefaultRotation *= _xRotator.rotation;
        if (_yRotator != null)
            _aimer.DefaultRotation *= _yRotator.rotation;

    }

    private void Start()
    {
        var range = Stats.GetStat<Range>();
        range.CurrentValue = range.CurrentValue;
    }

    void Update()
    {
        if (_finder.GetSingleTarget() == null) return;
        //put this in view class?
        Quaternion attackLook = _aimer == null ? 
            Quaternion.identity :
            _aimer.GetAttackLook(BulletPrefab, _finder.GetAvailableTargets());

        if(_yRotator != null)
            _yRotator.localEulerAngles = new Vector3(0,attackLook.eulerAngles.y,0);
        if(_xRotator != null)
            _xRotator.localEulerAngles = new Vector3(attackLook.x,0,0);

        _currCooldown += Time.deltaTime;
        if (_currCooldown < _attackCooldown.CurrentValue) return;
        _currCooldown = 0;
        Shoot();
    }

    //this will be abstract?
    void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab);

        Quaternion rotation;
        if (_aimer == null)
            rotation = Quaternion.identity;
        else
            rotation = _aimer.GetAttackLook(bullet, _finder.GetAvailableTargets());

        bullet.transform.SetPositionAndRotation(
            position: _bulletSpawnSpot.position,
            rotation: rotation
        );

        //bullet.transform.forward = rotation * Vector3.forward ;

        bullet.Sender = this;
        bullet.Init();

        _onTowerShoot?.Invoke();
    }

}

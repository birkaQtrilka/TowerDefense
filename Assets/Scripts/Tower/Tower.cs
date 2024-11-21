using UnityEngine;

public class Tower : MonoBehaviour//make it generic and accept scriptable objects
{
    //object pool?
    //data for bullet remove later
    [SerializeField] float speed;

    //data for tower
    [SerializeField] float _attackSpeed;
    [SerializeField] Bullet _bulletPrefab;


    [SerializeField] TargetFinder _finder;
    [SerializeField] Aimer _aimer;
    [SerializeField] Transform _bulletSpawnSpot;

    float _currSpeed;

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, _aimer.GetAttackLook(null, _finder.GetAvailableTargets()) * Vector3.forward);    
    }

    void Update()
    {
        if (_finder.GetSingleTarget() == null) return;
        //put this in view class?
        transform.rotation = _aimer.GetAttackLook(null, _finder.GetAvailableTargets());
        _currSpeed += Time.deltaTime;
        if (_currSpeed < _attackSpeed) return;
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
        //remove later
        (bullet as Projectile).Velocity = transform.forward * speed;
        Debug.DrawRay(bullet.transform.position, (bullet as Projectile).Velocity, Color.red, 2);
    }
}

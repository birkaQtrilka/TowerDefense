using UnityEngine;

public class Wobbler : MonoBehaviour
{
    [SerializeField] float _amplitude;
    [SerializeField] float _frequency;
    [SerializeField] Vector3 _direction;
    [SerializeField] Vector3 _rotationAxis;
    [SerializeField] float _rotationSpeed;

    Vector3 _pivot;
    
    void Awake()
    {
        _direction.Normalize();
        _rotationAxis.Normalize();
    }
    
    void Start()
    {
        _pivot = transform.position;
    }

    void FixedUpdate()
    {
        transform.SetPositionAndRotation
        (
            _pivot + _amplitude * Mathf.Sin(Time.time * _frequency) * _direction, 
            transform.rotation * Quaternion.AngleAxis(_rotationSpeed * Time.fixedDeltaTime, _rotationAxis)
        );
    }
}

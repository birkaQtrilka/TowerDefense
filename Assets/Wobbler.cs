using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbler : MonoBehaviour
{
    [SerializeField] float _amplitude;
    [SerializeField] float _frequency;
    [SerializeField] Vector3 _direction;

    Vector3 _pivot;
    
    void Awake()
    {
        _direction.Normalize();
    }
    
    void Start()
    {
        _pivot = transform.position;

    }

    void FixedUpdate()
    {
        transform.position = _pivot + _amplitude * Mathf.Sin(Time.time * _frequency) * _direction;
    }
}

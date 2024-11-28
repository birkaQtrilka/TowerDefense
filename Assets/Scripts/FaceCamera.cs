using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;        
    }

    void FixedUpdate()
    {
        Quaternion copy = transform.rotation;
        copy.SetLookRotation(( transform.position - _cam.transform.position).normalized);
        transform.rotation = copy;
    }
}

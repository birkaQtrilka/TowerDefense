using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//deprecated, it's a gameobject that is scaling up. Use particle system instead
public class ZoneVisual : MonoBehaviour
{
    [SerializeField] GameObject _sphere;
    [SerializeField] AnimationCurve _animationCurve;
    [SerializeField] float _finalSize;
    [SerializeField] float _animTime;

    public void StartVisual()
    {
        StartCoroutine(Visual());
    }

    IEnumerator Visual()
    {
        var copy = Instantiate(_sphere);
        copy.transform.position = transform.position;
        float time = 0;
        while(time < _animTime)
        {

            yield return null;
            time += Time.deltaTime;

            copy.transform.localScale = (time / _animTime) * _finalSize * Vector3.one;
        }
    }
}

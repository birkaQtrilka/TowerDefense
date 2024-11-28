using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField] float _time;

    float _currTime;

    void OnEnable()
    {
        _currTime = 0;    
    }
    
    void Update()
    {
        _currTime += Time.deltaTime;
        if (_currTime < _time) return;

        Destroy(gameObject);
    }
}

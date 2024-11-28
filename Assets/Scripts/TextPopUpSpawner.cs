using System.Collections;
using TMPro;
using UnityEngine;

public abstract class TextPopUpSpawner : MonoBehaviour
{

    [SerializeField] TextMeshPro _popUpPrefab;
    [Header("Text will move in up direction of spawn point")]
    [SerializeField] Transform _spawnPoint;
    [SerializeField] float _riseTime;
    [SerializeField] float _riseSpeed;
    [SerializeField] float _wobbleAmplitude;
    [SerializeField] float _wobbleSpeed;
    
    protected void ShowText(string str)
    {
        //done this so the coroutine doesn't stop if the parent dies
        var txt = Instantiate(_popUpPrefab);
        txt.text = str;
        txt.StartCoroutine(TextUpdate(txt));
    }

    IEnumerator TextUpdate(TextMeshPro txt)
    {
        float currTime = 0;
        Transform txtTransform = txt.transform;
        txtTransform.position = _spawnPoint.position;
        Vector3 up = txtTransform.up;
        Vector3 right = txtTransform.right;
        while (currTime < _riseTime)
        {
            currTime += Time.deltaTime;
            txtTransform.position += _riseSpeed * up + Mathf.Sin(Time.time * _wobbleSpeed) * _wobbleAmplitude * right;
            txt.alpha = 1 - currTime / _riseTime;

            yield return null;
        }
        Destroy(txtTransform.gameObject);
    }

}

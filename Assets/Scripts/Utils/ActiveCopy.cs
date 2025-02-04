using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveCopy : MonoBehaviour
{
    [SerializeField] GameObject _mirroredObject;


    bool _main = true;
    GameObject _alwaysActiveObject;


    void Start()
    {
        if (!_main) return;

        _alwaysActiveObject = new GameObject("Always Active Object");

        ActiveCopy monobehavior = _alwaysActiveObject.AddComponent<ActiveCopy>();
        monobehavior._main = false;
        monobehavior.StartCoroutine(UpdateCR());
    }

    IEnumerator UpdateCR()
    {
        while (_main)
        {
            if (_mirroredObject != null)
            {
                gameObject.SetActive(_mirroredObject.activeInHierarchy);
            }
            yield return null;
        }
    }

}

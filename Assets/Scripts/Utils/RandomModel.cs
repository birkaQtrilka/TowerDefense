using UnityEngine;

//spawns different models and rotates them randomly
[ExecuteInEditMode]
public class RandomModel : MonoBehaviour
{
    [SerializeField] GameObject[] _models;

    void Start()
    {
        SelectRandom();
    }

    void OnEnable()
    {
        SelectRandom();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SelectRandom();
    }

    void SelectRandom()
    {
        if (_models == null || _models.Length == 0) return;
        foreach (var m in _models)
        {
            m.SetActive(false);
        }
        var model = _models[Random.Range(0, _models.Length)];
        model.SetActive(true);
        model.transform.Rotate(0,90 * Random.Range(0, 4), 0);
    } 
}

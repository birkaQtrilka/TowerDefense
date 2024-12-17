using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] LifeManager _lifeManager;
    [SerializeField] Text _textMesh;
    Health _lives;

    void Awake()
    {
        _lives = _lifeManager.LifeAmount; 
    }

    void OnEnable()
    {
        _lives.OnCurrentUpdate.AddListener(UpdateVisual);
        UpdateVisual(0, 0);
    }

    void OnDisable()
    {
        _lives.OnCurrentUpdate.RemoveListener(UpdateVisual);
    }

    public void UpdateVisual(int old, int newVal)
    {
        _textMesh.text = $"{_lives.CurrentValue}/{_lives.MaxValue}";
    }
}

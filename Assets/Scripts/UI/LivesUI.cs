using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] LifeManager _lifeManager;
    [SerializeField] Text _textMesh;


    void OnEnable()
    {
        _lifeManager.LifeAmount.CurrentUpdated.AddListener(UpdateVisual);
        UpdateVisual(0, _lifeManager.LifeAmount);
    }

    void OnDisable()
    {
        _lifeManager.LifeAmount.CurrentUpdated.RemoveListener(UpdateVisual);
    }

    public void UpdateVisual(int old, Stat<int> lives)
    {
        _textMesh.text = $"{lives.CurrentValue}/{lives.MaxValue}";
    }
}

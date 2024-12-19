using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] Text _textMesh;
    public void UpdateUI(int curr, int max)
    {
        _textMesh.text = $"{curr}/{max}";
    }
}

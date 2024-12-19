using UnityEngine;
using UnityEngine.UI;

public class BuildTimerUI : MonoBehaviour
{
    [SerializeField] Text _textMesh;
    
    public void ShowTime(float time)
    {
        _textMesh.text = $"Next wave in {(int)time} s";
    }
}

using TMPro;
using UnityEngine;
/// <summary>
/// visualises tower data as floating text
/// </summary>
public class Tower3DSelection : SelectionVisual
{
    [SerializeField] GameObject _visual;
    [SerializeField] TextMeshPro _textMesh;
    [SerializeField] Tower _tower;

    public override void Select()
    {
        string text = "";
        _tower.Stats.DoForEach(s=> text += s.GetType().Name + ": " + s.ToString() +  '\n');
        _textMesh.text = text;

        _visual.SetActive(true);
    }
    
    public override void Deselect()
    {
        _visual.SetActive(false);
    }

}

//public interface IStatTextDisplayer<T> where T : Stat
//{
//    public string GetString
//}
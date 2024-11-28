using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StatsContainer))]
public class StatsContainerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        base.OnGUI(position, property, label);
    }
}

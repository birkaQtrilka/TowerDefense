using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
[CustomPropertyDrawer(typeof(EventFoldoutAttribute))]
public class EventFoldoutPropertyDrawer : PropertyDrawer
{
    UnityEventDrawer eventDrawer;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (eventDrawer == null)
            eventDrawer = new UnityEventDrawer();

        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        return eventDrawer.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (eventDrawer == null)
            eventDrawer = new UnityEventDrawer();

        float lineHeight = EditorGUIUtility.singleLineHeight;
        Rect foldoutRect = new(position.min.x, position.min.y, position.width, lineHeight);

        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, new GUIContent(label.text + " Event"));

        if (property.isExpanded)
            eventDrawer.OnGUI(new(position.x, position.y + lineHeight, position.width, position.height - lineHeight), property, label);
    }
}
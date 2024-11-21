using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

[CustomPropertyDrawer(typeof(StatsContainer))]
public class StatsDrawer : PropertyDrawer
{
    string _currentlySelectedType;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the "stats" list property
        SerializedProperty statsProperty = property.FindPropertyRelative("stats");
        if (statsProperty == null) return EditorGUIUtility.singleLineHeight;

        EditorGUI.GetPropertyHeight(statsProperty, label);
        // Height includes one line for the header and one line per list item + add button
        return EditorGUI.GetPropertyHeight(statsProperty, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty statsProperty = property.FindPropertyRelative("stats");
        if (statsProperty == null)
        {
            EditorGUI.LabelField(position, "Unable to find 'stats' property.");
            return;
        }
        property.serializedObject.Update(); // Ensure serializedObject is synced

        EditorGUILayout.LabelField(label);


        for (int i = 0; i < statsProperty.arraySize; i++)
        {
            SerializedProperty element = statsProperty.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(element, new GUIContent(element.type.Split('<', '>')[1]),true);

            // Draw Remove button
            if (GUILayout.Button("Remove", GUILayout.MaxWidth(100)))
            {
                statsProperty.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();

        }

        if (GUILayout.Button("Add New Stat"))
        {
            DrawMenu(statsProperty);
        }

        property.serializedObject.ApplyModifiedProperties();
    }

    void DrawMenu(SerializedProperty statsProperty)
    {
        var types = Assembly.GetAssembly(typeof(Stat)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Stat)));

        GenericMenu menu = new();
        foreach (var type in types)
            AddMenuItem(menu, type, statsProperty);

        menu.ShowAsContext();

    }

    void AddMenuItem(GenericMenu menu, Type type, SerializedProperty statsProperty)
    {
        // the menu item is marked as selected if it matches the current type name
        menu.AddItem
        (
            new GUIContent(type.Name),
            type.Name == _currentlySelectedType, 
            OnMenuButtonPress, 
            new MenuData(type, statsProperty)
        );
    }

    void OnMenuButtonPress(object menuDataObj)
    {
        MenuData data = menuDataObj as MenuData;
        data.statsProperty.serializedObject.Update();

        _currentlySelectedType = data.type.Name;
        SerializedProperty arrayProp = data.statsProperty;

        int newIndex = arrayProp.arraySize;
        arrayProp.InsertArrayElementAtIndex(newIndex);

        SerializedProperty newElement = arrayProp.GetArrayElementAtIndex(newIndex);

        object newStat = Activator.CreateInstance(data.type);

        newElement.managedReferenceValue = newStat;
        arrayProp.serializedObject.ApplyModifiedProperties();

    }

    class MenuData
    {
        public Type type;
        public SerializedProperty statsProperty;

        public MenuData(Type type, SerializedProperty statsProperty)
        {
            this.type = type;
            this.statsProperty = statsProperty;
        }
    }
}


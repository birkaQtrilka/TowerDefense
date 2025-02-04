using UnityEditor;

//easier way to acces SoundNames. You can use the scriptable object too
public class SoundNamesWindow : EditorWindow
{
    public string[] Names = new string[0];


    [MenuItem("Stefan/SoundNames")]
    public static void ShowWindow()
    {
        GetWindow<SoundNamesWindow>("SoundNames");
    }

    private void OnGUI()
    {
        SoundNamesContainer container = SoundNamesContainer.Instance;

        SerializedObject so = new (container);
        SerializedProperty stringsProperty = so.FindProperty("Names");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties(); 
    }
}

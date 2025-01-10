using UnityEditor;
using UnityEngine;

public class SoundNamesContainer : ScriptableObject
{

    private static SoundNamesContainer _instance; 
    public static SoundNamesContainer Instance 
    {
        get
        {
            if (_instance == null) 
            {
                _instance = Resources.Load<SoundNamesContainer>("SoundNames");
                
                if (_instance == null) 
                { 
                    _instance = CreateInstance<SoundNamesContainer>();
                    AssetDatabase.CreateAsset(_instance, "Assets/Resources/SoundNames.asset");
                    AssetDatabase.SaveAssets();
                } 
            } 
            return _instance; 
        }
    }

    public string[] Names;
}

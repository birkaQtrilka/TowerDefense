using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SoundPair
{
    public SoundName name;
    public SoundData data;
}

[CreateAssetMenu(menuName ="SoundCollection")]
public class Sounds : ScriptableObject
{
    public Dictionary<string, SoundData> SoundClips;
    [SerializeField] List<SoundPair> _inspectorSoundClips = new();

    
    void Reset()
    {
        UdpateDictionary();
    }

    void OnEnable()
    {
        UdpateDictionary();

    }

    public void UdpateDictionary()
    {
        for (int i = 0; i < _inspectorSoundClips.Count - 1; i++)
        {
            for (int j = i + 1; j < _inspectorSoundClips.Count; j++)
            {
                if (_inspectorSoundClips[i].name.Name != _inspectorSoundClips[j].name.Name) continue;

                _inspectorSoundClips.RemoveAt(j);
                Debug.LogError($"item {i} and item {j} have the same name. This isn't allowed. Deleting item {j}");
                j--;
            }
        }
        SoundClips = _inspectorSoundClips.ToDictionary(t => t.name.Name, t => t.data);
    }
}

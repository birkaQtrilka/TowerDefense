using System;
using UnityEngine;
//used in dictionary to preset certain clip values before playing them
[Serializable]
public class SoundData
{
    [Range(0f, 1f)] public float Volume = 1;
    public AudioClip Clip;
    public bool Loop;
}

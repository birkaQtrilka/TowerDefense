using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        //Debug.Log("song awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroying song manager");
            Destroy(this);
            return;
        }
        else
        {
            _inactiveSources = new Stack<AudioSource>(GetComponents<AudioSource>());
            foreach (AudioSource source in _inactiveSources)
                InitSource(source);
            Instance = this;

            DontDestroyOnLoad(this);
        }

    }
    #endregion

    //my object pool
    readonly List<SourceInfo> _activeSources = new();
    Stack<AudioSource> _inactiveSources;
    int _currentID;
    [SerializeField, Range(0, 1)] float _masterVolume;
    [SerializeField] Sounds _data;
    //make this for inspector interface, but habe actual dictionary be a copy of it with strings as key


    //SoundData from here is general, so you don't have to put the same volume of a sound in 100 different scripts.
    //However, you CAN have custom volume/soundData in each object in case you want to make one more quiet than the others
    //Use SoundAdapter if you want to play sounds onclick or on other UnityEvent calls
    public static int PlayRandomSound(SoundName[] names, float volumeMult = 1)
    {
        return Instance.PlaySound(names[UnityEngine.Random.Range(0, names.Length)], volumeMult);
    }

    public static int PlayRandomSound(SoundName[] names, Action onComplete, float volumeMult = 1)
    {
        return Instance.PlaySound(names[UnityEngine.Random.Range(0, names.Length)], onComplete, volumeMult);
    }

    public bool ContainsSoundWithID(int id)
    {
        return _activeSources.Any(s => s.ID == id);
    }



    (int, AudioSource) PlaySound(string soundName, float volumeMult = 1)
    {
        if (!_data.SoundClips.ContainsKey(soundName))
        {
            _data.UdpateDictionary();
            if (!_data.SoundClips.ContainsKey(soundName))
            {
                Debug.LogError($"There is no sound named {soundName}");
                return (-1, null);
            }
        }
        int soundID = _currentID++;
        AudioSource source = GetSource(soundID);
        SoundData data = _data.SoundClips[soundName];
        source.clip = data.Clip;
        source.volume = data.Volume * volumeMult * _masterVolume;
        source.loop = data.Loop;
        source.Play();

        return (soundID, source);
    }

    IEnumerator WaitForSoundEnd(AudioSource source, Action onComplete)
    {
        yield return new WaitUntil(() => source.time >= source.clip.length || (source.time == 0 && !source.isPlaying));
        onComplete?.Invoke();
    }

    public int PlaySound(SoundName soundName, float volumeMult = 1)
    {
        (int id, _) = PlaySound(soundName.Name, volumeMult);
        return id;
    }

    public int PlaySound(SoundName soundName, Action onComplete, float volumeMult = 1)
    {
        (int id, AudioSource audioSource) = PlaySound(soundName.Name, volumeMult);
        StartCoroutine(WaitForSoundEnd(audioSource, onComplete));
        return id;
    }

    public void StopSound(int id)
    {
        SourceInfo source = FindSourceInfoByID(id);
        if (source != null)
        {
            source?.AudioSource.Stop();
            ReleaseSource(source);
        }
    }

    void InitSource(AudioSource source)
    {
        source.playOnAwake = false;
    }

    AudioSource GetSource(int id)
    {
        AudioSource source;
        if (_inactiveSources.Count == 0)
        {
            source = gameObject.AddComponent<AudioSource>();
            InitSource(source);
        }
        else
            source = _inactiveSources.Pop();

        _activeSources.Add(new SourceInfo(source, id));

        source.loop = false;
        source.volume = _masterVolume;
        source.enabled = true;

        return source;

    }

    SourceInfo FindSourceInfoByID(int id)
    {
        return _activeSources.FirstOrDefault(s => s.ID == id);
    }

    void ReleaseSource(SourceInfo audioSource)
    {
        _activeSources.Remove(audioSource);
        audioSource.AudioSource.enabled = false;
        _inactiveSources.Push(audioSource.AudioSource);
    }

    void Update()
    {
        for (int i = 0; i < _activeSources.Count; i++)
        {
            var source = _activeSources[i];
            bool soundEnded = source.AudioSource.time >= source.AudioSource.clip.length || (source.AudioSource.time == 0 && !source.AudioSource.isPlaying);

            if (soundEnded)
            {
                ReleaseSource(source);
                i--;
            }
        }
    }
}
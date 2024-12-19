using UnityEngine;

public class SoundAdapter : MonoBehaviour
{
    [SerializeField] SoundName _soundName;
    [SerializeField] float _volume = 1;

    int _soundID = -1;

    public void PlaySound()
    {
        _soundID = SoundManager.Instance.PlaySound(_soundName,_volume);
    }

    public void StopSound()
    {
        SoundManager.Instance.StopSound(_soundID);
    }
}

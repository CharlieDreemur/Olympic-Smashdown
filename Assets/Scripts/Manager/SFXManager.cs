using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : Singleton<SFXManager>
{
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;
    private float _volume = 1;

    [SerializeField] Slider _slider;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float num)
    {
        _volume = num;
        _audioSource.volume = _volume;
    }

    public void SetVolumeToSlider()
    {
        if (_slider == null) { return; }
        SetVolume(_slider.value);
    }

    public void Mute()
    {
        _audioSource.mute = true;
    }

    public void UnMute()
    {
        _audioSource.mute = false;
    }

    public float GetVolume()
    {
        return _volume;
    }

    public static void PlayMusic(string SFXName)
    {
        if (Instance.sfxClips.ContainsKey(SFXName))
        {
            Instance._audioSource.loop = false;
            Instance._audioSource.PlayOneShot(Instance.sfxClips[SFXName]);
        }
    }

    public static void PlayMusicLoop(string SFXName)
    {
        if (Instance.sfxClips.ContainsKey(SFXName))
        {
            Instance._audioSource.loop = true;
            Instance._audioSource.PlayOneShot(Instance.sfxClips[SFXName]);
        }
    }

    public static void StopMusic(string SFXName)
    {
        if (Instance.sfxClips.ContainsKey(SFXName))
        {
            Instance._audioSource.clip = Instance.sfxClips[SFXName];
            Instance._audioSource.Stop();
        }
    }
}

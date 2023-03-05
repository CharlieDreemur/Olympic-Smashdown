using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(AudioSource))]
public class SFXManager : Singleton<SFXManager>
{
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;
    private float _volume = 1;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float num)
    {
        _volume = num;
        _audioSource.volume = _volume;
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
            Instance._audioSource.clip = Instance.sfxClips[SFXName];
            Instance._audioSource.loop = false;
            Instance._audioSource.Play();
        }
    }

    public static void PlayMusicLoop(string SFXName)
    {
        if (Instance.sfxClips.ContainsKey(SFXName))
        {
            Instance._audioSource.clip = Instance.sfxClips[SFXName];
            Instance._audioSource.loop = true;
            Instance._audioSource.Play();
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
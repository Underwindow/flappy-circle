using UnityEngine;
using System;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            sound.InitByAudioSource(audioSource, PlayerPrefs.GetInt("IS_SOUND_ON", 1));
        }
    }

    public Sound PlaySound(AudioClip clip, uint delaySec)
    {
        var s = GetSound(clip);
            s.source.Play(delaySec * 44100);
       
        return s;
    }

    public Sound PlaySound(AudioClip clip)
    {
        return PlaySound(clip, 0);
    }

    public void PlayClip(AudioClip clip)
    {
        PlaySound(clip);
    }

    public Sound GetSound(AudioClip clip)
    {
        var s = Array.Find(sounds, sound => sound.clip.name == clip.name);
        
        if (s == null)
        {
            Debug.LogError($"AudioClip:{s.clip.name} Not Found");
        }
        
        return s;
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(.1f, 3f)]
    public float pitch = 1;

    public bool loop = false;
    public bool playOnAwake = false;

    [HideInInspector]
    public AudioSource source;

    public void InitByAudioSource(AudioSource audioSource, float userVolume = 1)
    {
        source = audioSource;
        source.clip = clip;
        source.volume = volume * userVolume;
        source.pitch = pitch;
        source.loop = loop;
        source.playOnAwake = playOnAwake;
    }

    public float GetTime()
        => source.clip.length * (1 / source.pitch);
}

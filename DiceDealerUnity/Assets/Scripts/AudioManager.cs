using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private bool _sfxMuted;
    private bool _musicMuted;
    public AudioSource spawnAudioSource { get; private set; }

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip[0];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.name.Equals("Spawn"))
            {
                spawnAudioSource = s.source;
            }
        }

        Play("Background");
    }

    public void Play(string name)
    {
        if (!_sfxMuted)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound with " + name + " not found!");
                return;
            }

            if (s.clip.Length > 1)
            {
                s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            }

            if (s.restartOnPlay)
            {
                s.source.Play();
            }
            else if (!s.source.isPlaying)
            {
                if (name.Equals("AutoSpawn") || name.Equals("Spawn"))
                {
                    s.source.pitch = Random.Range(0.75f, 1.05f);
                }

                s.source.Play();
            }
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with " + name + " not found!");
            return;
        }

        s.source.Pause();
    }

    [CanBeNull]
    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void Mute()
    {
        _sfxMuted = true;
        foreach (var sound in sounds)
        {
            sound.source.mute = _sfxMuted;
        }
    }

    public void UnMute()
    {
        _sfxMuted = false;
        foreach (var sound in sounds)
        {
            sound.source.mute = _sfxMuted;
        }
    }
}
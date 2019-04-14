using System;
using UnityEngine;
 

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private bool _sfxMuted;
    private bool _musicMuted;
    // Use this for initialization
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip[0];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
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

            if(s.clip.Length > 1)
            {
                s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            }

            if(s.restartOnPlay)
            {
                s.source.Play();
            }
            else if(!s.source.isPlaying)
            {
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


}

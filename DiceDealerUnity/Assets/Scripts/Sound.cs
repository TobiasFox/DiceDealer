using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  {

    public string name;
    public AudioClip[] clip;

    [Range(0f,1f)]
    public float volume = 1;
    [Range(.1f, 3f)]
    public float pitch = 1;
    public bool loop;
    [Tooltip("if false True make sure the sound is looping")]
    public bool restartOnPlay = true;

    [HideInInspector]
    public AudioSource source; 
}

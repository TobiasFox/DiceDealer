using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{
    [SerializeField] private Sprite spriteMusicOff;
    [SerializeField] private Sprite spriteMusicOn;
    private Image buttonImage;
    private bool musicMuted;
    private AudioManager audioManager;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ChangeSprite()
    {
        buttonImage.sprite = musicMuted ? spriteMusicOn : spriteMusicOff;
        if (musicMuted)
        {
            audioManager.UnMute();
        }
        else
        {
            audioManager.Mute();
        }

        musicMuted = !musicMuted;
    }
}
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

    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ChangeSprite()
    {
        buttonImage.sprite = musicMuted ? spriteMusicOn : spriteMusicOff;
        musicMuted = !musicMuted;
    }
}
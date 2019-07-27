using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager getInstance()
    {
        return instance;
    }

    [SerializeField] private AudioSource screenAudioSource;
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource bgmAudioSource;
    //[SerializeField] private AudioClip bgm;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //playBGM();
    }

    public void clickScreenSound()
    {
        screenAudioSource.Play();
    }

    public void clickButtonSound()
    {
        //uiAudioSource.Play();
    }

    public void playBGM()
    {
        bgmAudioSource.Play();
    }
}

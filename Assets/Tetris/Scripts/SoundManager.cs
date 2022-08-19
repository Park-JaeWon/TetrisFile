using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipStart;
    public AudioClip audioClipDrop;
    public AudioClip audioClipLine;
    public AudioClip audioClipOver;

    public static SoundManager instance; //외부에서 접근o

    void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }

        
    }

    public void PlayStartSound()
    {
        audioSource.PlayOneShot(audioClipStart);
    }
    public void PlayDropSound()
    {
        audioSource.PlayOneShot(audioClipDrop);
    }

    public void PlayLineSound()
    {
        audioSource.PlayOneShot(audioClipLine);
    }
    public void PlayOverSound()
    {
        audioSource.PlayOneShot(audioClipOver);
    }

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

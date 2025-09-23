using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip cardSoundClip;
    public AudioClip matchSoundClip;
    public AudioClip missmatchSoundClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CardSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void MatchSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void NotMatchSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}

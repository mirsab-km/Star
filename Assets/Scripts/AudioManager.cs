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
    public AudioClip victorySoundClip;
    public AudioClip gameOverSoundClip;

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

    public void CardSound()
    {
        audioSource.PlayOneShot(cardSoundClip);
    }

    public void MatchSound()
    {
        audioSource.PlayOneShot(matchSoundClip);
    }

    public void NotMatchSound()
    {
        audioSource.PlayOneShot(missmatchSoundClip);
    }

    public void VictorySound()
    {
        audioSource.PlayOneShot(victorySoundClip);
    }

    public void GameOverSound()
    {
        audioSource.PlayOneShot(gameOverSoundClip);
    }
}

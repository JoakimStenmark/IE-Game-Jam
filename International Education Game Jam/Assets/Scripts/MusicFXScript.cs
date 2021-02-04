using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFXScript : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    [SerializeField] private AudioClip[] audioEffects;
    [SerializeField] private AudioClip[] music;

    public static MusicFXScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundEffect(int clipIndex)
    {
        effectsSource.PlayOneShot(audioEffects[clipIndex]);
    }

    public void SetMusic(int musicIndex)
    {
        musicSource.Stop();
        musicSource.clip = music[musicIndex];
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
    }

    public AudioSource ManipulateMusicSource()
    {
        return musicSource;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- SFX Source ---")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip playershoot;
    public AudioClip enemyshoot;
    public AudioClip playerhit;
    public AudioClip enemydeath;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.volume = 0.2f;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioClip musicMenu, musicForest, musicCementary, musicTheatre;
    [SerializeField] private float fadeTime;
    [SerializeField] public AudioSource audioSrc;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
            audioSrc.Play();

            return;
        }

        Destroy(gameObject);
    }
    public void ChangeMusic(int sceneNumber)
    {
        switch (sceneNumber)
        {
            case 0:
                if (audioSrc.clip != musicMenu)
                {
                    StartCoroutine(FadeOut(audioSrc, fadeTime, musicMenu));
                }
                break;
            case 1:
                if (audioSrc.clip != musicForest)
                {
                    StartCoroutine(FadeOut(audioSrc, fadeTime, musicForest));
                }
                break;
            case 2:
                if (audioSrc.clip != musicCementary)
                {
                    StartCoroutine(FadeOut(audioSrc, fadeTime, musicCementary));
                }
                break;
            case 3:
                if (audioSrc.clip != musicTheatre)
                {
                    StartCoroutine(FadeOut(audioSrc, fadeTime, musicTheatre));
                }
                break;
            default:
                break;
        }
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, AudioClip clip)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.clip = clip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [HideInInspector] public AudioSource audioSrc;

    [SerializeField] private AudioClip uiClick, shoot, hit, hitGhost, maskOn, teleport, damadge, enemyDeath, jump, dash, collect; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        audioSrc = GetComponent<AudioSource>();
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            MusicManager.Instance.ChangeMusic(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "ui":      // to do
                audioSrc.PlayOneShot(uiClick);
                break;
            case "shoot":
                audioSrc.PlayOneShot(shoot);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "hitGhost":
                audioSrc.PlayOneShot(hitGhost);
                break;
            case "maskOn":
                audioSrc.PlayOneShot(maskOn);
                break;
            case "teleport":
                audioSrc.PlayOneShot(teleport);
                break;
            case "damadge": // to do
                audioSrc.PlayOneShot(damadge);
                break;
            case "enemyDeath":
                audioSrc.PlayOneShot(enemyDeath);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "dash":
                audioSrc.PlayOneShot(dash);
                break;
            case "collect": 
                audioSrc.PlayOneShot(collect);
                break;
            default:
                break;
        }
    }
}

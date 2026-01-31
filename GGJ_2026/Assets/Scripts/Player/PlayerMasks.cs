using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum Mask
{
    None,
    Forest,
    Cementary,
    Circus
}

public class PlayerMasks : MonoBehaviour
{
    public static PlayerMasks Instance;

    public PlayerStats ps;

    [HideInInspector] public Mask currentMask = Mask.None;

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
    }
    private void Start()
    {
        ChangePlayer();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && ps.forestUnlocked)
        {
            currentMask = Mask.Forest;
            PlayerControler.Instance.animCurrent = PlayerControler.Instance.animForest;
            if (PlayerControler.Instance.spriteCurrent.flipX)
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteForest;
                PlayerControler.Instance.spriteCurrent.flipX = true;
            }
            else
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteForest;
                PlayerControler.Instance.spriteCurrent.flipX = false;
            }
            ChangePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && ps.cementaryUnlocked)
        {
            currentMask = Mask.Cementary;
            PlayerControler.Instance.animCurrent = PlayerControler.Instance.animCementary;
            if (PlayerControler.Instance.spriteCurrent.flipX)
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteCementary;
                PlayerControler.Instance.spriteCurrent.flipX = true;
            }
            else
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteCementary;
                PlayerControler.Instance.spriteCurrent.flipX = false;
            }
            ChangePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && ps.circusUnlocked)
        {
            currentMask = Mask.Circus;
            PlayerControler.Instance.animCurrent = PlayerControler.Instance.animCircus;
            if (PlayerControler.Instance.spriteCurrent.flipX)
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteCircus;
                PlayerControler.Instance.spriteCurrent.flipX = true;
            }
            else
            {
                PlayerControler.Instance.spriteCurrent = PlayerControler.Instance.spriteCircus;
                PlayerControler.Instance.spriteCurrent.flipX = false;
            }
            ChangePlayer();
        }
    }
    private void ChangePlayer()
    {
        PlayerControler.Instance.animDefault.gameObject.SetActive(false);
        PlayerControler.Instance.animForest.gameObject.SetActive(false);
        PlayerControler.Instance.animCementary.gameObject.SetActive(false);
        PlayerControler.Instance.animCircus.gameObject.SetActive(false);

        PlayerControler.Instance.animCurrent.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public MaskUI maskForest, maskCementary, maskCircus;

    [SerializeField] private GameObject life1Broken, life2Broken, life3Broken, life4Broken, life4;

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
        UpdateHP();
        UpdateMasks();
        maskForest.max = PlayerControler.Instance.ps.atkNormalCD;
        maskForest.fillcounter.SetActive(false);
        maskCementary.max = PlayerControler.Instance.ps.atkNormalCD;
        maskCementary.fillcounter.SetActive(false);
        maskCircus.max = PlayerControler.Instance.ps.atkCD;
        maskCircus.fillcounter.SetActive(false);
    }
    private void Update()
    {
        UpdateHP();
        UpdateMasks();
    }
    private void UpdateHP()
    {
        if (PlayerControler.Instance.ps.maxHP == 4)
        {
            life4.SetActive(true);
        }
        else
        {
            life4.SetActive(false);
        }
        switch (PlayerControler.Instance.ps.currentHP)
        {
            case 4:
                life4Broken.SetActive(false);
                life3Broken.SetActive(false);
                life2Broken.SetActive(false);
                life1Broken.SetActive(false);
                break;
            case 3:
                if (PlayerControler.Instance.ps.maxHP == 4)
                {
                    life4Broken.SetActive(true);
                }
                life3Broken.SetActive(false);
                life2Broken.SetActive(false);
                life1Broken.SetActive(false);
                break;
            case 2:
                if (PlayerControler.Instance.ps.maxHP == 4)
                {
                    life4Broken.SetActive(true);
                }
                life3Broken.SetActive(true);
                life2Broken.SetActive(false);
                life1Broken.SetActive(false);
                break;
            case 1:
                if (PlayerControler.Instance.ps.maxHP == 4)
                {
                    life4Broken.SetActive(true);
                }
                life3Broken.SetActive(true);
                life2Broken.SetActive(true);
                life1Broken.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void UpdateMasks()
    {
        if (!PlayerControler.Instance.ps.forestUnlocked)
        {
            maskForest.active.SetActive(false);
            maskForest.number.SetActive(false);
        }
        else
        {
            if(PlayerMasks.Instance.currentMask == Mask.Forest)
            {
                maskForest.active.SetActive(true);
            }
            else
            {
                maskForest.active.SetActive(false);
                maskForest.number.SetActive(true);
            }
        }

        if (!PlayerControler.Instance.ps.cementaryUnlocked)
        {
            maskCementary.active.SetActive(false);
            maskCementary.number.SetActive(false);
        }
        else
        {
            if (PlayerMasks.Instance.currentMask == Mask.Cementary)
            {
                maskCementary.active.SetActive(true);
            }
            else
            {
                maskCementary.active.SetActive(false);
                maskCementary.number.SetActive(true);
            }
        }

        if (!PlayerControler.Instance.ps.circusUnlocked)
        {
            maskCircus.active.SetActive(false);
            maskCircus.number.SetActive(false);
        }
        else
        {
            if (PlayerMasks.Instance.currentMask == Mask.Circus)
            {
                maskCircus.active.SetActive(true);
            }
            else
            {
                maskCircus.active.SetActive(false);
                maskCircus.number.SetActive(true);
            }
        }
    }
}

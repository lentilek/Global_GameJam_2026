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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && ps.forestUnlocked)
        {
            currentMask = Mask.Forest;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && ps.cementaryUnlocked)
        {
            currentMask = Mask.Cementary;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && ps.circusUnlocked)
        {
            currentMask = Mask.Circus;
        }
    }
}

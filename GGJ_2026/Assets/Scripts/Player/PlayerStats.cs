using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerStats : ScriptableObject
{
    public int maxHP;
    public int currentHP;
    public float speedNormal;
    public float speedAir;
    public float dashForce;
    public float dashCD;
    public float jumpForce;
    public int atkNormal;
    public float atkNormalCD;
    public int atkFireball;
    public float atkCD;

    public bool forestUnlocked;
    public bool cementaryUnlocked;
    public bool circusUnlocked;

    public Mask currentMask;

    public void SetUp(PlayerStats statsMain,PlayerStats statsNew)
    {
        statsNew.maxHP = statsMain.maxHP;
        statsNew.currentHP = statsMain.currentHP;
        statsNew.speedNormal = statsMain.speedNormal;
        statsNew.speedAir = statsMain.speedAir;
        statsNew.dashCD = statsMain.dashCD;
        statsNew.dashCD = statsMain.dashCD;
        statsNew.jumpForce = statsMain.jumpForce;
        statsNew.atkNormal = statsMain.atkNormal;
        statsNew.atkNormalCD = statsMain.atkNormalCD;
        statsNew.atkFireball = statsMain.atkFireball;
        statsNew.atkCD = statsMain.atkCD;

        statsNew.forestUnlocked = statsMain.forestUnlocked;
        statsNew.cementaryUnlocked = statsMain.cementaryUnlocked;
        statsNew.circusUnlocked = statsMain.circusUnlocked;

        statsNew.currentMask = statsMain.currentMask;
    }
}

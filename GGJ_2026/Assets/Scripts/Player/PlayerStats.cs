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
}

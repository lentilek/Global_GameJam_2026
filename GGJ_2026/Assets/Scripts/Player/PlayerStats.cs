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
    public float speedDash;
    public float jumpForce;
    public int atkNormal;
    public int atkFireball;
    public float atkCD;
}

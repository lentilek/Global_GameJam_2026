using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatManager : MonoBehaviour
{
    [SerializeField] private int maxHP;
    public int currentHP;
    public int attackDMG;

    private void Awake()
    {
        currentHP = maxHP;
    }
    private void Update()
    {
        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

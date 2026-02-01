using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    private void OnTriggerEnter(Collider other)
    {
        if (Boss.Instance.lastAttackIndex == 2 && _animator.GetInteger("ActionType") == 2)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerControler.Instance.ps.currentHP -= Boss.Instance.chargeDMG;
            }
        }
    }
}

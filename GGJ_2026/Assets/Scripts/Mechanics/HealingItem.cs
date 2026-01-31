using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && PlayerControler.Instance.ps.currentHP < PlayerControler.Instance.ps.maxHP)
        {
            PlayerControler.Instance.ps.currentHP += healAmount;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUpgrade : MonoBehaviour
{
    [SerializeField] private int hpAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PlayerControler.Instance.ps.hpUpgrade)
        {
            PlayerControler.Instance.ps.hpUpgrade = true;
            PlayerControler.Instance.ps.maxHP += hpAmount;
            PlayerControler.Instance.ps.currentHP += hpAmount;
            Destroy(gameObject);
        }
    }
}

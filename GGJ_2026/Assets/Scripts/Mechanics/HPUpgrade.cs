using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUpgrade : MonoBehaviour
{
    [SerializeField] private int hpAmount;

    private void Start()
    {
        if (PlayerControler.Instance.ps.hpUpgrade)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PlayerControler.Instance.ps.hpUpgrade)
        {
            AudioManager.Instance.PlaySound("collect");
            PlayerControler.Instance.ps.hpUpgrade = true;
            PlayerControler.Instance.ps.maxHP += hpAmount;
            PlayerControler.Instance.ps.currentHP += hpAmount;
            Destroy(gameObject);
        }
    }
}

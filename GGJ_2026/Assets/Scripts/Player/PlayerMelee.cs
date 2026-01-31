using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{    
    [HideInInspector] public bool onCD;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "Enemy" && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponent<EnemyStatManager>().currentHP -= PlayerControler.Instance.ps.atkNormal;
            PlayerControler.Instance.AttackCDStart();
        }
    }
}

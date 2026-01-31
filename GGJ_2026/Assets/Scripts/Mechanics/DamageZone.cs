using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damageCD;
    [SerializeField] private int damage;

    private bool onCD;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !onCD)
        {
            StartCoroutine(DamageCD());
        }
    }

    IEnumerator DamageCD()
    {
        onCD = true;
        PlayerControler.Instance.ps.currentHP -= damage;
        yield return new WaitForSeconds(damageCD);
        onCD = false;
    }
}

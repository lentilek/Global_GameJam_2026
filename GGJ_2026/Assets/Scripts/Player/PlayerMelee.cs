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
        if (other.gameObject.tag == "Enemy" && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponent<EnemyStatManager>().currentHP -= PlayerControler.Instance.ps.atkNormal;
            PlayerControler.Instance.AttackCDStart();
        }
        else if (other.gameObject.tag == "Destroy" && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponentInParent<Destructable>().Destroy();
            PlayerControler.Instance.AttackCDStart();
        }
        else if (other.gameObject.tag == "Ghost" && PlayerMasks.Instance.currentMask == Mask.Cementary && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponentInParent<Destructable>().Destroy();
            PlayerControler.Instance.AttackCDStart();
        }
    }
}

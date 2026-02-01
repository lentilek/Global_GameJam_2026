using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (other.gameObject.tag == "Enemy" && 
            (PlayerMasks.Instance.currentMask == Mask.Forest || PlayerMasks.Instance.currentMask == Mask.None) && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponent<EnemyStatManager>().currentHP -= PlayerControler.Instance.ps.atkNormal;
            PlayerControler.Instance.AttackCDStart();
        }
        else if (other.gameObject.tag == "Destroy" &&
            (PlayerMasks.Instance.currentMask == Mask.Forest || PlayerMasks.Instance.currentMask == Mask.None) && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponentInParent<Destructable>().Destroy();
            PlayerControler.Instance.AttackCDStart();
        }
        else if (other.gameObject.tag == "Ghost" && PlayerMasks.Instance.currentMask == Mask.Cementary && !onCD)
        {
            onCD = true;
            if(other.gameObject.GetComponent<EnemyStatManager>() != null) other.gameObject.GetComponent<EnemyStatManager>().currentHP -= PlayerControler.Instance.ps.atkNormal;
            else if(other.gameObject.GetComponent<BossBullet>() != null)
            {
               Destroy(other.gameObject);
            }
            PlayerControler.Instance.AttackCDStart();
        }
    }
}

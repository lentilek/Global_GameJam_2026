using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGUpgrade : MonoBehaviour
{
    [SerializeField] private int dmgAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PlayerControler.Instance.ps.dmgUpgrade)
        {
            PlayerControler.Instance.ps.dmgUpgrade = true;
            PlayerControler.Instance.ps.atkNormal += dmgAmount;
            PlayerControler.Instance.ps.atkFireball += dmgAmount;
            Destroy(gameObject);
        }
    }
}

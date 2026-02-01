using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGUpgrade : MonoBehaviour
{
    [SerializeField] private int dmgAmount;
    private void Start()
    {
        if (PlayerControler.Instance.ps.dmgUpgrade)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PlayerControler.Instance.ps.dmgUpgrade)
        {
            AudioManager.Instance.PlaySound("collect");
            PlayerControler.Instance.ps.dmgUpgrade = true;
            PlayerControler.Instance.ps.atkNormal += dmgAmount;
            PlayerControler.Instance.ps.atkFireball += dmgAmount;
            Destroy(gameObject);
        }
    }
}

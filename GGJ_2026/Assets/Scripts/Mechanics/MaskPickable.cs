using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickable : MonoBehaviour
{
    public int maskNumber; // 1 - forest, 2 - cementary, 3 - circus
    [SerializeField] private GameObject child;

    private void Start()
    {
        switch(maskNumber)
        {
            case 1:
                if(PlayerControler.Instance.ps.forestUnlocked)
                {
                    Destroy(gameObject);
                }
                else
                {
                    child.SetActive(true);
                }
                break;
            case 2:
                if (PlayerControler.Instance.ps.cementaryUnlocked)
                {
                    Destroy(gameObject);
                }
                else
                {
                    child.SetActive(true);
                }
                break;
            case 3:
                if(PlayerControler.Instance.ps.circusUnlocked)
                {
                    Destroy(gameObject);
                }
                else
                {
                    child.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (maskNumber)
            {
                case 1:
                    PlayerControler.Instance.ps.forestUnlocked = true;
                    break;
                case 2:
                    PlayerControler.Instance.ps.cementaryUnlocked = true;
                    break;
                case 3:
                    PlayerControler.Instance.ps.circusUnlocked = true;
                    break;
                default:
                    break;
            }
            AudioManager.Instance.PlaySound("collect");
            Destroy(gameObject);
        }
    }
}

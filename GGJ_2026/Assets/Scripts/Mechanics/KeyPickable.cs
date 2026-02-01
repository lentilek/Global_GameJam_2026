using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyPickable : MonoBehaviour
{
    public int keyNumber; // 1 - forest, 2 - cementary, 3 - circus
    [SerializeField] private GameObject child;
    private void Start()
    {
        switch (keyNumber)
        {
            case 1:
                if (PlayerControler.Instance.ps.mask1)
                {
                    Destroy(gameObject);
                }
                else
                {
                    child.SetActive(true);
                }
                break;
            case 2:
                if (PlayerControler.Instance.ps.mask2)
                {
                    Destroy(gameObject);
                }
                else
                {
                    child.SetActive(true);
                }
                break;
            case 3:
                if (PlayerControler.Instance.ps.mask3)
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
        if(other.gameObject.tag == "Player")
        {
            switch(keyNumber)
            {
                case 1:
                    PlayerControler.Instance.ps.mask1 = true;
                    break;
                case 2:
                    PlayerControler.Instance.ps.mask2 = true;
                    break;
                case 3:
                    PlayerControler.Instance.ps.mask3 = true;
                    break;
                default:
                    break;
            }
            AudioManager.Instance.PlaySound("collect");
            Destroy(gameObject);
        }
    }
}

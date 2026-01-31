using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickable : MonoBehaviour
{
    public int keyNumber; // 1 - forest, 2 - cementary, 3 - circus

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
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickable : MonoBehaviour
{
    public int maskNumber; // 1 - forest, 2 - cementary, 3 - circus

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
            Destroy(gameObject);
        }
    }
}

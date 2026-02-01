using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportingDoor : MonoBehaviour
{
    [SerializeField] private int teleportToIndex;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            //AudioManager.Instance.PlaySound("teleport");
            PlayerControler.Instance.ps.currentMask = PlayerMasks.Instance.currentMask;
            SceneManager.LoadScene(teleportToIndex);
        }
    }
}

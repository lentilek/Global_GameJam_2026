using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWall : MonoBehaviour
{
    [SerializeField] private Collider colliderWall;

    private void Update()
    {
        if(PlayerMasks.Instance.currentMask == Mask.Cementary)
        {
            colliderWall.enabled = false;
        }
        else
        {
            colliderWall.enabled = true;
        }
    }
}

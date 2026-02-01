using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossArena : MonoBehaviour
{
    [SerializeField] private GameObject platform, colliders;
    [SerializeField] private Transform endPlatform;
    [SerializeField] private float animTime;
    private void Start()
    {
        StartCoroutine(MoveElevator());
    }

    IEnumerator MoveElevator()
    {
        platform.transform.DOMoveY(endPlatform.position.y, animTime);
        yield return new WaitForSeconds(animTime);
        colliders.SetActive(false);

    }
}

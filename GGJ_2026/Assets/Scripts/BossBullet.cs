using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float delay;

    private void Awake()
    {
        StartCoroutine(Delay());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControler.Instance.ps.currentHP -= Boss.Instance.shootDMG;
            Destroy(gameObject);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

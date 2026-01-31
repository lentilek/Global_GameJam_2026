using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public static PlayerMelee Instance;
    
    [HideInInspector] public bool onCD;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !onCD)
        {
            onCD = true;
            other.gameObject.GetComponent<EnemyStatManager>().currentHP -= PlayerControler.Instance.ps.atkNormal;
            PlayerControler.Instance.AttackCDStart();
        }
    }
}

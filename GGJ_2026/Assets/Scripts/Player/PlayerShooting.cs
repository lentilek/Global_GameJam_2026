using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;

    [SerializeField] private GameObject bulletPrefab;
    private bool canUShoot;

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
        canUShoot = true;
    }

    public void Shoot(Transform transform)
    {
        if (canUShoot && Time.timeScale == 1)
        {
            canUShoot = false;
            StartCoroutine(CantShoot());
            GameObject cb = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            Rigidbody rig = cb.GetComponent<Rigidbody>();

            rig.AddForce(transform.right * PlayerControler.Instance.ps.bulletSpeed, ForceMode.VelocityChange);
            //rig.AddForce(new Vector3(ps.dashForce, 0, 0), ForceMode.Impulse);
        }
    }
    IEnumerator CantShoot()
    {
        yield return new WaitForSeconds(PlayerControler.Instance.ps.atkCD);
        canUShoot = true;
        GameUI.Instance.maskCircus.fillcounter.SetActive(false);
    }
}

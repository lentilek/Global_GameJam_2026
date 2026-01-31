using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler Instance;

    public PlayerStats ps;
    private Rigidbody rb;
    [HideInInspector] public bool isOnGround;
    public bool doubleJump, dash;
    private bool jumped, dashed;
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
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumped = false;
        dashed = false;
        onCD = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isOnGround)
            {
                transform.position += Vector3.right * ps.speedNormal * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * ps.speedAir * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isOnGround)
            {
                transform.position -= Vector3.right * ps.speedNormal * Time.deltaTime;
            }
            else
            {
                transform.position -= Vector3.right * ps.speedAir * Time.deltaTime;
            }
        }
        if (doubleJump)
        {
            if (!isOnGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !jumped)
            {
                jumped = true;
                rb.AddForce(new Vector3(0, ps.jumpForce, 0), ForceMode.Impulse);
            }
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isOnGround)
        {
            isOnGround = false;
            jumped = false;
            rb.AddForce(new Vector3(0, ps.jumpForce, 0), ForceMode.Impulse);
        }
        if (dash && Input.GetKeyDown(KeyCode.LeftShift) && !dashed)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector3(ps.dashForce, 0, 0), ForceMode.Impulse);
                StartCoroutine(Dash());
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector3(-ps.dashForce, 0, 0), ForceMode.Impulse);
                StartCoroutine (Dash());
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onCD)
        {
            PlayerMelee.Instance.gameObject.SetActive(true);
            PlayerMelee.Instance.onCD = false;
            StartCoroutine(AttackCDNoEnemy());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    IEnumerator Dash()
    {
        dashed = true;
        yield return new WaitForSeconds(ps.dashCD);
        dashed = false;
    }
    public void AttackCDStart()
    {
        StartCoroutine(AttackCD());
    }
    IEnumerator AttackCD()
    {
        onCD = true;
        yield return new WaitForSeconds(.1f);
        PlayerMelee.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);// ps.atkNormalCD);
        onCD = false;
    }
    IEnumerator AttackCDNoEnemy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (!onCD)
        {
            onCD = true;
            yield return new WaitForSeconds(.1f);
            PlayerMelee.Instance.gameObject.SetActive(false);
            yield return new WaitForSeconds(.3f);// ps.atkNormalCD);
            onCD = false;
        }
    }
}

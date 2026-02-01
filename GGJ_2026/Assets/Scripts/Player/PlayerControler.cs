using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler Instance;

    public PlayerStats ps;
    public Transform playerSpawnPoint;
    private Rigidbody rb;
    [HideInInspector] public bool isOnGround;
    private bool jumped, dashed;
    [HideInInspector] public bool onCD, onCDFire;

    [SerializeField] private PlayerMelee meleeLeft, meleeRight;
    [SerializeField] private GameObject fireLeft, fireRight;
    [SerializeField] private GameObject dashLeft, dashRight;

    public Animator animDefault, animForest, animCementary, animCircus, animCurrent;
    public SpriteRenderer spriteDefault, spriteForest, spriteCementary, spriteCircus, spriteCurrent;

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
        gameObject.transform.position = playerSpawnPoint.position;
        jumped = false;
        dashed = false;
        onCD = false;
        onCDFire = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isOnGround)
            {
                transform.position += Vector3.right * ps.speedNormal * Time.deltaTime;
                if (!animCurrent.GetBool("isSlashing")) animCurrent.SetBool("isRunning", true);
            }
            else
            {
                transform.position += Vector3.right * ps.speedAir * Time.deltaTime;
            }
            spriteCurrent.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isOnGround)
            {
                transform.position -= Vector3.right * ps.speedNormal * Time.deltaTime;
                if (!animCurrent.GetBool("isSlashing")) animCurrent.SetBool("isRunning", true);
            }
            else
            {
                transform.position -= Vector3.right * ps.speedAir * Time.deltaTime;
            }
            spriteCurrent.flipX = true;
        }
        else
        {
            animCurrent.SetBool("isRunning", false);
        }

        if (PlayerMasks.Instance.currentMask == Mask.Circus)
        {
            if (!isOnGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) 
                || Input.GetKeyDown(KeyCode.Space)) && !jumped)
            {
                AudioManager.Instance.PlaySound("jump");
                jumped = true;
                rb.AddForce(new Vector3(0, ps.jumpForce, 0), ForceMode.Impulse);
            }
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isOnGround)
        {
            AudioManager.Instance.PlaySound("jump");
            isOnGround = false;
            jumped = false;
            rb.AddForce(new Vector3(0, ps.jumpForce, 0), ForceMode.Impulse);
        }
        if (PlayerMasks.Instance.currentMask == Mask.Forest && Input.GetKeyDown(KeyCode.LeftShift) && !dashed)
        {
            AudioManager.Instance.PlaySound("dash");
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector3(ps.dashForce, 0, 0), ForceMode.Impulse);
                dashRight.gameObject.SetActive(true);
                StartCoroutine(Dash());
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector3(-ps.dashForce, 0, 0), ForceMode.Impulse);
                dashLeft.gameObject.SetActive(true);
                StartCoroutine(Dash());
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onCD && 
            (PlayerMasks.Instance.currentMask == Mask.Forest || PlayerMasks.Instance.currentMask == Mask.Cementary))// && isOnGround)
        {
            if (spriteCurrent.flipX)
            {
                meleeLeft.gameObject.SetActive(true);
                meleeLeft.onCD = false;
            }
            else
            {
                meleeRight.gameObject.SetActive(true);
                meleeRight.onCD = false;
            }
            animCurrent.SetBool("isSlashing", true);
            if(PlayerMasks.Instance.currentMask == Mask.Forest)
            {
                AudioManager.Instance.PlaySound("hit");
                GameUI.Instance.maskForest.fillcounter.SetActive(true);
                GameUI.Instance.maskForest.amount = GameUI.Instance.maskForest.max;
            }
            else if(PlayerMasks.Instance.currentMask == Mask.Cementary)
            {
                AudioManager.Instance.PlaySound("hitGhost");
                GameUI.Instance.maskCementary.fillcounter.SetActive(true);
                GameUI.Instance.maskCementary.amount = GameUI.Instance.maskCementary.max;
            }
            StartCoroutine(AttackCDNoEnemy());
        }else if(PlayerMasks.Instance.currentMask == Mask.Circus && Input.GetKeyDown(KeyCode.Mouse0) && !onCDFire)
        {
            if (spriteCurrent.flipX)
            {
                PlayerShooting.Instance.Shoot(fireLeft.transform, false);
            }
            else
            {
                PlayerShooting.Instance.Shoot(fireRight.transform, true);
            }
            onCDFire = true;
            GameUI.Instance.maskCircus.fillcounter.SetActive(true);
            GameUI.Instance.maskCircus.amount = GameUI.Instance.maskCircus.max;
        }

        if (isOnGround)
        {
            animCurrent.SetBool("isJumping", false);
        }
        else
        {
            animCurrent.SetBool("isRunning", false);
            if (!animCurrent.GetBool("isSlashing"))
            {
                animCurrent.SetBool("isJumping", true);
            }
            else
            {
                animCurrent.SetBool("isJumping", false);
            }
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
        yield return new WaitForSeconds(.2f);
        dashLeft.gameObject.SetActive(false);
        dashRight.gameObject.SetActive(false);
        yield return new WaitForSeconds(ps.dashCD-.2f);
        dashed = false;
    }
    public void AttackCDStart()
    {
        StartCoroutine(AttackCD());
    }
    IEnumerator AttackCD()
    {
        onCD = true;
        yield return new WaitForSeconds(.05f);
        meleeLeft.gameObject.SetActive(false);
        meleeRight.gameObject.SetActive(false);
        animCurrent.SetBool("isSlashing", false);

        GameUI.Instance.maskForest.fillcounter.SetActive(false);
        GameUI.Instance.maskCementary.fillcounter.SetActive(false);

        yield return new WaitForSeconds(ps.atkNormalCD);
        onCD = false;
    }
    IEnumerator AttackCDNoEnemy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (!onCD)
        {
            onCD = true;
            yield return new WaitForSeconds(.05f);
            animCurrent.SetBool("isSlashing", false);

            GameUI.Instance.maskForest.fillcounter.SetActive(false);
            GameUI.Instance.maskCementary.fillcounter.SetActive(false);

            meleeLeft.gameObject.SetActive(false);
            meleeRight.gameObject.SetActive(false);
            yield return new WaitForSeconds(ps.atkNormalCD);
            onCD = false;
        }
    }
}

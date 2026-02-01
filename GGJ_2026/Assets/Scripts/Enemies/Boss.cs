using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance;

    private EnemyStatManager _esm;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    [SerializeField] private int meleeDMG;
    [SerializeField] private float meleeCD, meleeLength;
    [SerializeField] private GameObject fanLeft, fanRight;

    [SerializeField] private Transform pointLeft, pointRight;
    public int chargeDMG;
    [SerializeField] private float chargeCD, chargeLength;
    [SerializeField] private BoxCollider _collider;

    [SerializeField] private GameObject bulletPrefab, bulletspawn;
    public int shootDMG;
    [SerializeField] private float shootCD, shootLength, bulletSpeed;

    [HideInInspector] public int lastAttackIndex;
    private List<int> actions = new List<int>();
    private bool onCD;

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
        onCD = false;
        lastAttackIndex = -1;
        actions.Add(1);
        actions.Add(2);
        actions.Add(3);
        _esm = GetComponent<EnemyStatManager>();
    }
    void Start()
    {
        GameUI.Instance.BossStart();
        StartCoroutine(StartFight());
    }
    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(5f);
        RandomAttack();
    }
    private void Update()
    {
        if(_esm.currentHP > 50)
        {
            GameUI.Instance.GetCurrentFill(_esm.currentHP - 50, 50, GameUI.Instance.fill1);
        }
        else if(_esm.currentHP > 0)
        {
            GameUI.Instance.GetCurrentFill(_esm.currentHP, 50, GameUI.Instance.fill2);
        }
        else
        {
            Time.timeScale = 0f;
            GameUI.Instance.endUI.SetActive(true);
        }
    }
    private void RandomAttack()
    {
        var temp = actions[Random.Range(0, actions.Count)];
        if(lastAttackIndex != -1) actions.Add(lastAttackIndex);
        lastAttackIndex = temp;
        actions.Remove(temp);
        switch(lastAttackIndex)
        {
            case 1:
                _animator.SetInteger("ActionType", 1);
                StartCoroutine(Melee());
                break;
            case 2:
                _animator.SetInteger("ActionType", 2);
                if (_sprite.flipX)
                {
                    gameObject.transform.DOMove(pointLeft.position, chargeLength);
                }
                else
                {
                    gameObject.transform.DOMove(pointRight.position, chargeLength);
                }
                StartCoroutine(Charge());
                break;
            case 3:
                _animator.SetInteger("ActionType", 3);
                if (_sprite.flipX)
                {
                    Shoot(bulletspawn.transform, false);
                }
                else
                {
                    Shoot(bulletspawn.transform, true);
                }
                break;
            default:
                break;
        }
    }
    IEnumerator Melee()
    {
        onCD = true;
        yield return new WaitForSeconds(meleeLength/2);
        if (_sprite.flipX)
        {
            fanLeft.SetActive(true);
        }
        else
        {
            fanRight.SetActive(true);
        }
        yield return new WaitForSeconds(meleeLength / 2);
        fanRight.SetActive(false);
        fanLeft.SetActive(false);
        _animator.SetInteger("ActionType", 0);
        yield return new WaitForSeconds(meleeCD);
        onCD = false;
        RandomAttack();
    }
    IEnumerator Charge()
    {
        onCD = true;
        _collider.enabled = false;
        yield return new WaitForSeconds(chargeLength);
        _sprite.flipX = !_sprite.flipX;
        _animator.SetInteger("ActionType", 0);
        _collider.enabled = true;
        yield return new WaitForSeconds(chargeCD);
        onCD = false;
        RandomAttack();
    }
    public void Shoot(Transform transform, bool right)
    {
        if (!onCD && Time.timeScale == 1)
        {
            onCD = true;
            StartCoroutine(CantShoot());
            GameObject cb = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            Rigidbody rig = cb.GetComponent<Rigidbody>();

            if (right) rig.AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
            else rig.AddForce(-transform.right * bulletSpeed, ForceMode.VelocityChange);
        }
    }
    IEnumerator CantShoot()
    {
        yield return new WaitForSeconds(shootLength);
        _animator.SetInteger("ActionType", 0);
        yield return new WaitForSeconds(shootCD);
        onCD = false;
        RandomAttack();
    }
}
